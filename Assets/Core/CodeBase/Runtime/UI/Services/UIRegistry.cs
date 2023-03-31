using System.Collections.Generic;

namespace WC.Runtime.UI.Services
{
  public class UIRegistry : IUIRegistry
  {
    public MainUI UI { get; set; }
    public GameplayHUD HUD { get; set; }
    
    private readonly Dictionary<UIWindowID, WindowBase> _uiWindows = new();
    private readonly Dictionary<HUDWindowID, WindowBase> _hudWindows = new();
    private readonly Dictionary<UIPanelID, PanelBase> _uiPanels = new();
    private readonly Dictionary<HUDPanelID, PanelBase> _hudPanels = new();
    
    
    public void Register(UIWindowID id, WindowBase window) => _uiWindows.Add(id, window);
    public void Register(HUDWindowID id, WindowBase window) => _hudWindows.Add(id, window);
    public void Register(UIPanelID id, PanelBase panel) => _uiPanels.Add(id, panel);
    public void Register(HUDPanelID id, PanelBase panel) => _hudPanels.Add(id, panel);
    
    public void Unregister(UIWindowID id) => _uiWindows.Remove(id);
    public void Unregister(HUDWindowID id) => _hudWindows.Remove(id);
    public void Unregister(UIPanelID id) => _uiPanels.Remove(id);
    public void Unregister(HUDPanelID id) => _hudPanels.Remove(id);

    public WindowBase Get(UIWindowID id)
    {
      WindowBase window = _uiWindows[id];
      
      //TODO Логгер
      // if (window == null) 
      //   LogService.Log<UIRegistry>($"Окна {id} нет в регистре!", LogLevel.Error);

      return window;
    }

    public WindowBase Get(HUDWindowID id)
    {
      WindowBase window = _hudWindows[id];
      
      //TODO Логгер
      // if (window == null) 
      //   LogService.Log<UIRegistry>($"Окна {id} нет в регистре!", LogLevel.Error);

      return window;
    }

    public PanelBase Get(UIPanelID id)
    {
      PanelBase panel = _uiPanels[id];
      
      //TODO Логгер
      // if (panel == null) 
      //   LogService.Log<UIRegistry>($"Панели {id} нет в регистре!", LogLevel.Error);

      return panel;
    }

    public PanelBase Get(HUDPanelID id)
    {
      PanelBase panel = _hudPanels[id];
      
      //TODO Логгер
      // if (panel == null) 
      //   LogService.Log<UIRegistry>($"Панели {id} нет в регистре!", LogLevel.Error);

      return panel;
    }

    public bool TryGet(UIWindowID id, out WindowBase windowBase)
    {
      if (_uiWindows.TryGetValue(id, out windowBase)) return true;
      
      //TODO Логгер
      //LogService.Log<UIRegistry>($"Окна {id} нет в регистре!", LogLevel.Error);
      
      return false;
    }

    public bool TryGet(HUDWindowID id, out WindowBase windowBase)
    {
      if (_hudWindows.TryGetValue(id, out windowBase)) return true;
      
      //TODO Логгер
      //LogService.Log<UIRegistry>($"Окна {id} нет в регистре!", LogLevel.Error);
      
      return false;
    }

    public bool TryGet(UIPanelID id, out PanelBase panelBase)
    {
      if (_uiPanels.TryGetValue(id, out panelBase)) return true;
      
      //TODO Логгер
      //LogService.Log<UIRegistry>($"Панели {id} нет в регистре!", LogLevel.Error);
      
      return false;
    }

    public bool TryGet(HUDPanelID id, out PanelBase panelBase)
    {
      if (_hudPanels.TryGetValue(id, out panelBase)) return true;
      
      //TODO Логгер
      //LogService.Log<UIRegistry>($"Панели {id} нет в регистре!", LogLevel.Error);
      
      return false;
    }

    public void CleanUp()
    {
      UI = null;
      HUD = null;
      
      _uiWindows.Clear();
      _hudWindows.Clear();
      _uiPanels.Clear();
      _hudPanels.Clear();
    }
  }
}