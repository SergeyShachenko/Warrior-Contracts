namespace WC.Runtime.UI.Services
{
  public class WindowService : IWindowService
  {
    private readonly IUIFactory _uiFactory;
    private readonly IHUDFactory _hudFactory;

    public WindowService(IUIFactory uiFactory, IHUDFactory hudFactory)
    {
      _uiFactory = uiFactory;
      _hudFactory = hudFactory;
    }


    public void Open(UIWindowID id)
    {
      if (_uiFactory.Registry.Windows.TryGetValue(id, out WindowBase window) && window.IsVisible == false) 
        window.Open();
    }

    public void Open(HUDWindowID id)
    {
      if (_hudFactory.Registry.Windows.TryGetValue(id, out WindowBase window) && window.IsVisible == false) 
        window.Open();
    }

    public void Close(UIWindowID id)
    {
      if (_uiFactory.Registry.Windows.TryGetValue(id, out WindowBase window) && window.IsVisible) 
        window.Close();
    }

    public void Close(HUDWindowID id)
    {
      if (_hudFactory.Registry.Windows.TryGetValue(id, out WindowBase window) && window.IsVisible) 
        window.Close();
    }
  }
}