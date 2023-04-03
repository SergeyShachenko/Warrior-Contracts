using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Services
{
  public class UIFactory : FactoryBase,
    IUIFactory
  {
    private readonly IUIRegistry _registry;

    private Transform _windowsParent, _panelsParent;

    public UIFactory(
      IAssetsProvider assetsProvider,
      ISaveLoadRegistry saveLoadRegistry,
      IUIRegistry registry) 
      : base(assetsProvider, saveLoadRegistry)
    {
      _registry = registry;
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
        
        _panelsParent = new GameObject("Panels").transform;
        _panelsParent.parent = uiParent;
      }
      
      if (uiObj.TryGetComponent(out MainUI mainUI))
        _registry.Register(mainUI);
      //TODO Логгер
      // else
      //   LogService.Log<UIFactory>("Отсутствует компонент - MainUI!", LogLevel.Error);

      return _registry.UI;
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
      
      _registry.Register(id, window);

      return _registry.Get(id);
    }
    
    public async Task<PanelBase> Create(UIPanelID id)
    {
      PanelBase panel = null;
      
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
      
      _registry.Register(id, panel);

      return _registry.Get(id);
    }

    public override async Task WarmUp()
    {
      await p_AssetsProvider.Load<GameObject>(AssetAddress.UI.MainUI);
      await p_AssetsProvider.Load<GameObject>(AssetAddress.UI.HUD.Windows.Shop);
    }
  }
}