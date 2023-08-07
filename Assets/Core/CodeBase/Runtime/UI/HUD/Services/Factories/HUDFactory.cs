using System;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Services
{
  public class HUDFactory : FactoryBase<HUDRegistry>,
    IHUDFactory,
    IDisposable,
    IWarmUp
  {
    private readonly IAssetsProvider _assetsProvider;
    private readonly IServiceManager _serviceManager;
    private readonly IStaticDataService _staticDataService;

    private Transform _windowsParent, _screensParent;

    public HUDFactory(
      ISaveLoadService saveLoadService,
      IAssetsProvider assetsProvider,
      IServiceManager serviceManager,
      IStaticDataService staticDataService)
      : base(saveLoadService)
    {
      _assetsProvider = assetsProvider;
      _serviceManager = serviceManager;
      _staticDataService = staticDataService;

      serviceManager.Register(this);
    }
    

    public async Task<GameplayHUD> CreateHUD()
    {
      GameObject hudObj = await _assetsProvider.InstantiateAsync(AssetAddress.UI.HUD.GameplayHUD);
      Transform hudParent = hudObj.transform;
      var gameplayHUD = hudObj.GetComponent<GameplayHUD>();
      
      _windowsParent = new GameObject("Windows").transform;
      _windowsParent.parent = hudParent;
      
      _screensParent = new GameObject("Screens").transform;
      _screensParent.parent = hudParent;

      RegisterProgressWatcher(hudObj);
      Registry.Register(gameplayHUD);
      return Registry.HUD;
    }
    
    public async Task<WindowBase> Create(HUDWindowID id)
    {
      GameObject windowObj = await _assetsProvider.InstantiateAsync(_staticDataService.HUDWindows[id], _windowsParent);
      var window = windowObj.GetComponent<WindowBase>();
      
      Registry.Register(id, window);
      return window;
    }
    
    public async Task<ScreenBase> Create(HUDScreenID id)
    {
      GameObject screenObj = await _assetsProvider.InstantiateAsync(_staticDataService.HUDScreens[id], _screensParent);
      var screen = screenObj.GetComponent<ScreenBase>();
      
      Registry.Register(id, screen);
      return screen;
    }

    async Task IWarmUp.WarmUp() => await _assetsProvider.Load<GameObject>(AssetAddress.UI.HUD.GameplayHUD);
    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}