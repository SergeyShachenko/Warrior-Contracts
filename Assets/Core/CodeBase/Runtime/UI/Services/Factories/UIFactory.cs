using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Services
{
  public class UIFactory : FactoryBase<UIRegistry>,
    IUIFactory
  {
    private Transform _windowsParent, _screensParent;

    public UIFactory(
      IServiceManager serviceManager,
      IAssetsProvider assetsProvider,
      ISaveLoadService saveLoadService) 
      : base(serviceManager, assetsProvider, saveLoadService)
    {
      
    }
    
    
    public async Task<MainUI> CreateUI()
    {
      GameObject uiObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.UI.MainUI);
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
      //TODO Логгер
      // else
      //   LogService.Log<UIFactory>("Отсутствует компонент - MainUI!", LogLevel.Error);

      return Registry.UI;
    }

    public async Task<WindowBase> Create(UIWindowID id)
    {
      WindowBase window = null;

      switch (id)
      {
        case UIWindowID.Shop:
        {
          GameObject windowObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.UI.HUD.Windows.Shop, _windowsParent);
          RegisterProgressWatcher(windowObj);

          window = windowObj.GetComponent<ShopWindow>();
        }
          break;
      }
      
      Registry.Register(id, window);
      return Registry.Windows[id];
    }
    
    public async Task<ScreenBase> Create(UIScreenID id)
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
      await p_AssetsProvider.Load<GameObject>(AssetAddress.UI.MainUI);
      await p_AssetsProvider.Load<GameObject>(AssetAddress.UI.HUD.Windows.Shop);
    }
  }
}