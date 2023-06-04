using System.Collections.Generic;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.UI.Services
{
  public class UIRegistry : IRegistry
  {
    public MainUI UI { get; private set; }
    
    public IReadOnlyDictionary<UIWindowID, WindowBase> Windows => new Dictionary<UIWindowID, WindowBase>(_windows);
    public IReadOnlyDictionary<UIScreenID, ScreenBase> Screens => new Dictionary<UIScreenID, ScreenBase>(_screens);

    private readonly Dictionary<UIWindowID, WindowBase> _windows = new();
    private readonly Dictionary<UIScreenID, ScreenBase> _screens = new();
    
    
    public void Register(MainUI ui) => UI = ui;
    public void Register(UIWindowID id, WindowBase window) => _windows.Add(id, window);
    public void Register(UIScreenID id, ScreenBase screen) => _screens.Add(id, screen);

    public void Unregister(UIWindowID id) => _windows.Remove(id);
    public void Unregister(UIScreenID id) => _screens.Remove(id);

    public void CleanUp()
    {
      UI = null;

      _windows.Clear();
      _screens.Clear();
    }
  }
}