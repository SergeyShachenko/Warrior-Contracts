using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.IAP;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Services
{
  public class UIFactory : IUIFactory
  {
    private readonly IAssetsProvider _assetsProviderProvider;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;
    private readonly IAdsService _adsService;
    private readonly IIAPService _iapService;

    private Transform _ui;

    public UIFactory(
      IAssetsProvider assetsProviderProvider,
      IStaticDataService staticData,
      IPersistentProgressService progressService,
      IAdsService adsService, 
      IIAPService iapService)
    {
      _assetsProviderProvider = assetsProviderProvider;
      _staticData = staticData;
      _progressService = progressService;
      _adsService = adsService;
      _iapService = iapService;
    }


    public async Task CreateUI()
    {
      GameObject instantiate = await _assetsProviderProvider.Instantiate(AssetAddress.UI);
      _ui =  instantiate.transform;
    }

    public void CreateShop()
    {
      WindowConfig windowConfig = _staticData.ForWindow(WindowID.Shop);
      var window = Object.Instantiate(windowConfig.Window, _ui) as ShopWindow;
      window.Construct(_adsService, _progressService, _iapService, _assetsProviderProvider);
    }
  }
}