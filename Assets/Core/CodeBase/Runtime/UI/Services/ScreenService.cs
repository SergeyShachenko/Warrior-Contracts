namespace WC.Runtime.UI.Services
{
  public class ScreenService : IScreenService
  {
    private readonly IUIFactory _uiFactory;
    private readonly IHUDFactory _hudFactory;

    public ScreenService(IUIFactory uiFactory, IHUDFactory hudFactory)
    {
      _uiFactory = uiFactory;
      _hudFactory = hudFactory;
    }


    public void Show(UIScreenID id)
    {
      if (_uiFactory.Registry.Screens.TryGetValue(id, out ScreenBase panel) && panel.IsVisible == false) 
        panel.Show();
    }

    public void Show(HUDScreenID id)
    {
      if (_hudFactory.Registry.Screens.TryGetValue(id, out ScreenBase panel) && panel.IsVisible == false) 
        panel.Show();
    }

    public void Hide(UIScreenID id)
    {
      if (_uiFactory.Registry.Screens.TryGetValue(id, out ScreenBase panel) && panel.IsVisible) 
        panel.Hide();
    }

    public void Hide(HUDScreenID id)
    {
      if (_hudFactory.Registry.Screens.TryGetValue(id, out ScreenBase panel) && panel.IsVisible) 
        panel.Hide();
    }
  }
}