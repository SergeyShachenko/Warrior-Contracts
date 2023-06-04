using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Elements
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

    
    public void SubscribeUpdates()
    {
      _iapService.Initizlized += Refresh;
      _progressService.Player.Purchase.Changed += Refresh;
    }
    
    public void UnsubscribeUpdates()
    {
      _iapService.Initizlized -= Refresh;
      _progressService.Player.Purchase.Changed -= Refresh;
    }

    public async void Refresh()
    {
      RefreshUnavailableObjs();

      if (_iapService.IsInitialized == false) return;


      ClearShopItems();
      await FillShopItems();
    }

    
    private void ClearShopItems()
    {
      foreach (GameObject shopItemObj in _shopItemObjs)
        Destroy(shopItemObj);
    }
    
    private void RefreshUnavailableObjs()
    {
      foreach (GameObject obj in _unavailableObjs)
        obj.SetActive(_iapService.IsInitialized == false);
    }

    private async Task FillShopItems()
    {
      foreach (ProductDescription productDescription in _iapService.GetProductDescriptions())
      {
        GameObject shopItemObj = await _assetsProvider.InstantiateAsync(AssetAddress.UI.HUD.Windows.ShopItem, _parent);

        var shopItem = shopItemObj.GetComponent<ShopItem>();
        shopItem.Construct(_iapService, _assetsProvider, productDescription);
        shopItem.Init();

        _shopItemObjs.Add(shopItemObj);
      }
    }
  }
}