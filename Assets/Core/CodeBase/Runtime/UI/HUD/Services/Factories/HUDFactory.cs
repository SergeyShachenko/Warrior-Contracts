using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Services
{
  public class HUDFactory : FactoryBase<HUDRegistry>,
    IHUDFactory
  {
    private Transform _windowsParent, _screensParent;

    public HUDFactory(
      IServiceManager serviceManager,
      IAssetsProvider assetsProvider,
      ISaveLoadService saveLoadService)
      : base(serviceManager, assetsProvider, saveLoadService)
    {
      
    }

    
    public async Task<GameplayHUD> CreateHUD()
    {
      GameObject hudObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.UI.HUD.GameplayHUD);
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
      //
      //     var inventoryWindow = inventoryObj.GetComponent<ShopWindow>();
      //     inventoryWindow.Construct();
      //
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
      //
      //     var resourcesPanel = panelObj.GetComponent<ResourcesPanel>();
      //     resourcesPanel.Construct();
      //
      //     panel = resourcesPanel;
      //   }
      //     break;
      // }
      
      Registry.Register(id, screen);

      return Registry.Screens[id];
    }

    public override async Task WarmUp()
    {
      await p_AssetsProvider.Load<GameObject>(AssetAddress.UI.HUD.GameplayHUD);
    }
  }
}