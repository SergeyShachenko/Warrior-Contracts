using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.IAP;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI
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
      _progressService.Progress.Purchase.Changed += RefreshAvailableItems;
    }

    public void CleanUp()
    {
      _iapService.Initizlized -= RefreshAvailableItems;
      _progressService.Progress.Purchase.Changed -= RefreshAvailableItems;
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
        GameObject shopItemObj = await _assetsProvider.Instantiate(AssetAddress.ShopItem, _parent);

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