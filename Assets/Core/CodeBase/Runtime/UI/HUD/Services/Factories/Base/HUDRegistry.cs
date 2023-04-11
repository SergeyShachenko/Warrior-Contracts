using System.Collections.Generic;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Services
{
  public class HUDRegistry : IRegistry
  {
    public GameplayHUD HUD { get; private set; }
    
    public IReadOnlyDictionary<HUDWindowID, WindowBase> Windows => new Dictionary<HUDWindowID, WindowBase>(_windows);
    public IReadOnlyDictionary<HUDPanelID, PanelBase> Panels => new Dictionary<HUDPanelID, PanelBase>(_panels);
    
    private readonly Dictionary<HUDWindowID, WindowBase> _windows = new();
    private readonly Dictionary<HUDPanelID, PanelBase> _panels = new();
    
    
    public void Register(GameplayHUD hud) => HUD = hud;
    public void Register(HUDWindowID id, WindowBase window) => _windows.Add(id, window);
    public void Register(HUDPanelID id, PanelBase panel) => _panels.Add(id, panel);
    
    public void Unregister(HUDWindowID id) => _windows.Remove(id);
    public void Unregister(HUDPanelID id) => _panels.Remove(id);

    public void CleanUp()
    {
      HUD = null;
      
      _windows.Clear();
      _panels.Clear();
    }
  }
}