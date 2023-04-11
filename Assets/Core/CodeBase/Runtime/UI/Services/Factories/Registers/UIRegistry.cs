using System.Collections.Generic;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Services
{
  public class UIRegistry : IRegistry
  {
    public MainUI UI { get; private set; }
    
    public IReadOnlyDictionary<UIWindowID, WindowBase> Windows => new Dictionary<UIWindowID, WindowBase>(_windows);
    public IReadOnlyDictionary<UIPanelID, PanelBase> Panels => new Dictionary<UIPanelID, PanelBase>(_panels);

    private readonly Dictionary<UIWindowID, WindowBase> _windows = new();
    private readonly Dictionary<UIPanelID, PanelBase> _panels = new();
    
    
    public void Register(MainUI ui) => UI = ui;
    public void Register(UIWindowID id, WindowBase window) => _windows.Add(id, window);
    public void Register(UIPanelID id, PanelBase panel) => _panels.Add(id, panel);

    public void Unregister(UIWindowID id) => _windows.Remove(id);
    public void Unregister(UIPanelID id) => _panels.Remove(id);

    public void CleanUp()
    {
      UI = null;

      _windows.Clear();
      _panels.Clear();
    }
  }
}