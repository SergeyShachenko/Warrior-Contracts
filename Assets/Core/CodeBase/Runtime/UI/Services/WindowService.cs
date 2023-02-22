using WC.Runtime.UI.Windows;

namespace WC.Runtime.UI.Services
{
  public class WindowService : IWindowService
  {
    private readonly IUIFactory _uiFactory;

    public WindowService(IUIFactory uiFactory)
    {
      _uiFactory = uiFactory;
    }

    
    public void Open(WindowID id)
    {
      switch (id)
      {
        case WindowID.None:
          break;
        case WindowID.Shop:
          _uiFactory.CreateShop();
          break;
      }
    }
  }
}