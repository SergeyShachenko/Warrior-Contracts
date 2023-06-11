using System;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Screens;

namespace WC.Runtime.UI.Services
{
  public class UIFactory : FactoryBase<UIRegistry>,
    IUIFactory,
    IDisposable,
    IWarmUp
  {
    private readonly IAssetsProvider _assetsProvider;
    private readonly IServiceManager _serviceManager;
    private readonly IStaticDataService _staticDataService;

    private Transform _windowsParent, _screensParent;

    public UIFactory(
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
    
    
    public async Task<MainUI> CreateUI()
    {
      GameObject uiObj = await _assetsProvider.InstantiateAsync(AssetAddress.UI.MainUI);
      RegisterProgressWatcher(uiObj);
      
      if (uiObj != null)
      {
        Transform uiParent = uiObj.transform;
        
        _windowsParent = new GameObject("Windows").transform;
        _windowsParent.parent = uiParent;
        
        _screensParent = new GameObject("Screens").transform;
        _screensParent.parent = uiParent;
      }
      
      if (uiObj.TryGetComponent(out MainUI mainUI))
        Registry.Register(mainUI);

      return Registry.UI;
    }

    public async Task<WindowBase> Create(UIWindowID id)
    {
      GameObject windowObj = await _assetsProvider.InstantiateAsync(_staticDataService.UIWindows[id], _windowsParent);
      var window = windowObj.GetComponent<WindowBase>();
      
      Registry.Register(id, window);
      return window;
    }
    
    public async Task<ScreenBase> Create(UIScreenID id)
    {
      GameObject screenObj = await _assetsProvider.InstantiateAsync(_staticDataService.UIScreens[id], _screensParent);
      var screen = screenObj.GetComponent<ScreenBase>();
      
      Registry.Register(id, screen);
      return screen;
    }

    async Task IWarmUp.WarmUp()
    {
      await _assetsProvider.Load<GameObject>(AssetAddress.UI.MainUI);
      await _assetsProvider.Load<GameObject>(AssetAddress.UI.Screen.Shop);
    }
    
    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}