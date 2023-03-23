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
    private readonly IPersistentProgressService _progress;
    private readonly IAdsService _adsService;
    private readonly IIAPService _iapService;

    private Transform _windowsParent, _panelsParent;
    

    public UIFactory(
      IAssetsProvider assetsProvider,
      ISaveLoadService saveLoadService,
      IUIRegistry registry,
      IPersistentProgressService progress, 
      IAdsService adsService, 
      IIAPService iapService) 
      : base(assetsProvider, saveLoadService)
    {
      _registry = registry;
      _progress = progress;
      _adsService = adsService;
      _iapService = iapService;
    }


    public async Task<MainUI> CreateUI()
    {
      GameObject uiObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.UI.MainUI);
      
      if (uiObj != null)
      {
        Transform uiParent = uiObj.transform;
        
        _windowsParent = new GameObject("Windows").transform;
        _windowsParent.parent = uiParent;
        
        _panelsParent = new GameObject("Panels").transform;
        _panelsParent.parent = uiParent;
      }
      
      if (uiObj.TryGetComponent(out MainUI mainUI))
        _registry.UI = mainUI;
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
          GameObject windowObj = await InstantiateAsync(AssetAddress.UI.HUD.Windows.Shop, _windowsParent);

          var shopWindow = windowObj.GetComponent<ShopWindow>();
          shopWindow.Construct(_adsService, _progress, _iapService, p_AssetsProvider);

          window = shopWindow;
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

    public override Task WarmUp() => 
      Task.CompletedTask;
  }
}