using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.UI.Services
{
  public class HUDFactory : FactoryBase,
    IHUDFactory
  {
    private readonly IUIRegistry _registry;
    private readonly IPersistentProgressService _progress;
    private readonly IWindowService _windowService;

    private Transform _windowsParent, _panelsParent;

    public HUDFactory(
      IAssetsProvider assetsProvider,
      IUIRegistry registry,
      IPersistentProgressService progress,
      IWindowService windowService)
      : base(assetsProvider)
    {
      _registry = registry;
      _progress = progress;
      _windowService = windowService;
    }

    
    public async Task<GameplayHUD> CreateHUD(Player player)
    {
      GameObject hudObj = await InstantiateAsync(AssetAddress.UI.HUD);

      if (hudObj != null)
      {
        Transform hudParent = hudObj.transform;
        
        _windowsParent = new GameObject("Windows").transform;
        _windowsParent.parent = hudParent;
        
        _panelsParent = new GameObject("Panels").transform;
        _panelsParent.parent = hudParent;
      }
      
      if (hudObj.TryGetComponent(out GameplayHUD gameplayHUD))
      {
        gameplayHUD.Construct(player, _progress, _windowService);
        _registry.HUD = gameplayHUD;
      }
      //TODO Логгер
      // else
      //   LogService.Log<UIFactory>("Отсутствует компонент - GameplayHUD!", LogLevel.Error);

      return _registry.HUD;
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
      
      _registry.Register(id, window);

      return _registry.Get(id);
    }
    
    public async Task<PanelBase> Create(HUDPanelID id)
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