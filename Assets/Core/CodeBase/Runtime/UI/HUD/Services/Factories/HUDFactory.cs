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

    private Transform _windowsParent, _screensParent;

    public HUDFactory(
      ISaveLoadService saveLoadService,
      IAssetsProvider assetsProvider,
      IServiceManager serviceManager)
      : base(saveLoadService)
    {
      _assetsProvider = assetsProvider;
      _serviceManager = serviceManager;
      
      serviceManager.Register(this);
    }

    
    public async Task<GameplayHUD> CreateHUD()
    {
      GameObject hudObj = await _assetsProvider.InstantiateAsync(AssetAddress.UI.HUD.GameplayHUD);
      RegisterProgressWatcher(hudObj);

      if (hudObj != null)
      {
        Transform hudParent = hudObj.transform;
        
        _windowsParent = new GameObject("Windows").transform;
        _windowsParent.parent = hudParent;
        
        _screensParent = new GameObject("Sreens").transform;
        _screensParent.parent = hudParent;
      }
      
      if (hudObj.TryGetComponent(out GameplayHUD gameplayHUD)) 
        Registry.Register(gameplayHUD);
      
      //TODO Логгер
      // else
      //   LogService.Log<UIFactory>("Отсутствует компонент - GameplayHUD!", LogLevel.Error);

      return Registry.HUD;
    }
    
    public async Task<WindowBase> Create(HUDWindowID id)
    {
      WindowBase window = null;

      // switch (id)
      // {
      //   case HUDWindowID.Inventory:
      //   {
      //     GameObject inventoryObj = await InstantiateAsync(AssetAddress.UI.Shop, _windowsParent);
      //     window = inventoryWindow;
      //   }
      //     break;
      // }
      
      Registry.Register(id, window);

      return Registry.Windows[id];
    }
    
    public async Task<ScreenBase> Create(HUDScreenID id)
    {
      ScreenBase screen = null;
      
      // switch (id)
      // {
      //   case UIPanelID.Resources:
      //   {
      //     GameObject panelObj = await InstantiateAsync(AssetAddress.UI.Shop, _panelsParent);
      //     panel = resourcesPanel;
      //   }
      //     break;
      // }
      
      Registry.Register(id, screen);

      return Registry.Screens[id];
    }

    async Task IWarmUp.WarmUp() => 
      await _assetsProvider.Load<GameObject>(AssetAddress.UI.HUD.GameplayHUD);

    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}