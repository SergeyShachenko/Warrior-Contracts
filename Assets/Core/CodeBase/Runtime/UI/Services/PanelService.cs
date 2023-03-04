using WC.Runtime.UI;

namespace WC.Runtime.UI.Services
{
  public class PanelService : IPanelService
  {
    private readonly IUIRegistry _registry;
    
    public PanelService(IUIRegistry registry) => 
      _registry = registry;

    
    public void Show(UIPanelID id)
    {
      if (_registry.TryGet(id, out PanelBase panel) && panel.IsVisible == false) 
        panel.Show();
    }

    public void Show(HUDPanelID id)
    {
      if (_registry.TryGet(id, out PanelBase panel) && panel.IsVisible == false) 
        panel.Show();
    }

    public void Hide(UIPanelID id)
    {
      if (_registry.TryGet(id, out PanelBase panel) && panel.IsVisible) 
        panel.Hide();
    }

    public void Hide(HUDPanelID id)
    {
      if (_registry.TryGet(id, out PanelBase panel) && panel.IsVisible) 
        panel.Hide();
    }
  }
}