using WC.Runtime.UI;

namespace WC.Runtime.UI.Services
{
  public class WindowService : IWindowService
  {
    private readonly IUIRegistry _registry;

    public WindowService(IUIRegistry registry) => 
      _registry = registry;

    
    public void Open(UIWindowID id)
    {
      if (_registry.TryGet(id, out WindowBase window) && window.IsVisible == false) 
        window.Open();
    }

    public void Open(HUDWindowID id)
    {
      if (_registry.TryGet(id, out WindowBase window) && window.IsVisible == false) 
        window.Open();
    }

    public void Close(UIWindowID id)
    {
      if (_registry.TryGet(id, out WindowBase window) && window.IsVisible) 
        window.Close();
    }

    public void Close(HUDWindowID id)
    {
      if (_registry.TryGet(id, out WindowBase window) && window.IsVisible) 
        window.Close();
    }
  }
}