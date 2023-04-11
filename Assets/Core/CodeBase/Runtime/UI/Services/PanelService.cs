namespace WC.Runtime.UI.Services
{
  public class PanelService : IPanelService
  {
    private readonly IUIFactory _uiFactory;
    private readonly IHUDFactory _hudFactory;

    public PanelService(IUIFactory uiFactory, IHUDFactory hudFactory)
    {
      _uiFactory = uiFactory;
      _hudFactory = hudFactory;
    }


    public void Show(UIPanelID id)
    {
      if (_uiFactory.Registry.Panels.TryGetValue(id, out PanelBase panel) && panel.IsVisible == false) 
        panel.Show();
    }

    public void Show(HUDPanelID id)
    {
      if (_hudFactory.Registry.Panels.TryGetValue(id, out PanelBase panel) && panel.IsVisible == false) 
        panel.Show();
    }

    public void Hide(UIPanelID id)
    {
      if (_uiFactory.Registry.Panels.TryGetValue(id, out PanelBase panel) && panel.IsVisible) 
        panel.Hide();
    }

    public void Hide(HUDPanelID id)
    {
      if (_hudFactory.Registry.Panels.TryGetValue(id, out PanelBase panel) && panel.IsVisible) 
        panel.Hide();
    }
  }
}