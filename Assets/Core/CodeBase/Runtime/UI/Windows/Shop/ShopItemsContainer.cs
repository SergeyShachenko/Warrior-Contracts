using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI
{
  public class ShopItemsContainer : MonoBehaviour
  {
    [SerializeField] private GameObject[] _unavailableObjs;
    [SerializeField] private Transform _parent;

    private readonly List<GameObject> _shopItemObjs = new();
    
    private IIAPService _iapService;
    private IPersistentProgressService _progressService;
    private IAssetsProvider _assetsProvider;

    public void Construct(IIAPService iapService, IPersistentProgressService progressService, IAssetsProvider assetsProviderProvider)
    {
      _iapService = iapService;
      _progressService = progressService;
      _assetsProvider = assetsProviderProvider;
    }

    public void Init() => 
      RefreshAvailableItems();

    
    public void Subscribe()
    {
      _iapService.Initizlized += RefreshAvailableItems;
      _progressService.Player.Purchase.Changed += RefreshAvailableItems;
    }

    public void CleanUp()
    {
      _iapService.Initizlized -= RefreshAvailableItems;
      _progressService.Player.Purchase.Changed -= RefreshAvailableItems;
    }

    private async void RefreshAvailableItems()
    {
      UpdateUnavailableObjs();

      if (_iapService.IsInitialized == false) return;


      ClearShopItems();
      await FillShopItems();
    }

    private void ClearShopItems()
    {
      foreach (GameObject shopItemObj in _shopItemObjs)
        Destroy(shopItemObj);
    }

    private async Task FillShopItems()
    {
      foreach (ProductDescription productDescription in _iapService.GetProductDescriptions())
      {
        GameObject shopItemObj = await _assetsProvider.InstantiateAsync(AssetAddress.UI.ShopItem, _parent);

        var shopItem = shopItemObj.GetComponent<ShopItem>();
        shopItem.Construct(_iapService, _assetsProvider, productDescription);
        shopItem.Init();

        _shopItemObjs.Add(shopItemObj);
      }
    }

    private void UpdateUnavailableObjs()
    {
      foreach (GameObject obj in _unavailableObjs)
        obj.SetActive(_iapService.IsInitialized == false);
    }
  }
}