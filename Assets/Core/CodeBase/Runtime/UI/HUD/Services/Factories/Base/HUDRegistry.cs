using System.Collections.Generic;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Services
{
  public class HUDRegistry : IRegistry
  {
    public GameplayHUD HUD { get; private set; }
    
    public IReadOnlyDictionary<HUDWindowID, WindowBase> Windows => new Dictionary<HUDWindowID, WindowBase>(_windows);
    public IReadOnlyDictionary<HUDScreenID, ScreenBase> Screens => new Dictionary<HUDScreenID, ScreenBase>(_screens);
    
    private readonly Dictionary<HUDWindowID, WindowBase> _windows = new();
    private readonly Dictionary<HUDScreenID, ScreenBase> _screens = new();
    
    
    public void Register(GameplayHUD hud) => HUD = hud;
    public void Register(HUDWindowID id, WindowBase window) => _windows.Add(id, window);
    public void Register(HUDScreenID id, ScreenBase screen) => _screens.Add(id, screen);
    
    public void Unregister(HUDWindowID id) => _windows.Remove(id);
    public void Unregister(HUDScreenID id) => _screens.Remove(id);

    public void CleanUp()
    {
      HUD = null;
      
      _windows.Clear();
      _screens.Clear();
    }
  }
}