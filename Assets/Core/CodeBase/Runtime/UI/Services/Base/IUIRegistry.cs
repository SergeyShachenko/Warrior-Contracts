using WC.Runtime.UI;

namespace WC.Runtime.UI.Services
{
  public interface IUIRegistry
  {
    MainUI UI { get; set; }
    GameplayHUD HUD { get; set; }

    void Register(UIWindowID id, WindowBase window);
    void Register(HUDWindowID id, WindowBase window);
    void Register(UIPanelID id, PanelBase panel);
    void Register(HUDPanelID id, PanelBase panel);

    void Unregister(UIWindowID id);
    void Unregister(HUDWindowID id);
    void Unregister(UIPanelID id);
    void Unregister(HUDPanelID id);

    WindowBase Get(UIWindowID id);
    WindowBase Get(HUDWindowID id);
    PanelBase Get(UIPanelID id);
    PanelBase Get(HUDPanelID id);
    
    bool TryGet(UIWindowID id, out WindowBase windowBase);
    bool TryGet(HUDWindowID id, out WindowBase windowBase);
    bool TryGet(UIPanelID id, out PanelBase panelBase);
    bool TryGet(HUDPanelID id, out PanelBase panelBase);

    void CleanUp();
  }
}