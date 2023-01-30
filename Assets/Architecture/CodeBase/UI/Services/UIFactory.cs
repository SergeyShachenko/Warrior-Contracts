using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Services
{
  public class UIFactory : IUIFactory
  {
    private readonly IAssets _assets;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;
    private readonly IAdsService _adsService;

    private Transform _ui;

    public UIFactory(
      IAssets assets, 
      IStaticDataService staticData, 
      IPersistentProgressService progressService,
      IAdsService adsService)
    {
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
      _adsService = adsService;
    }


    public async Task CreateUI()
    {
      GameObject instantiate = await _assets.Instantiate(AssetAddress.UI);
      _ui =  instantiate.transform;
    }

    public void CreateShop()
    {
      WindowConfig windowConfig = _staticData.ForWindow(WindowID.Shop);
      var window = Object.Instantiate(windowConfig.Window, _ui) as ShopWindow;
      window.Construct(_adsService, _progressService);
    }
  }
}