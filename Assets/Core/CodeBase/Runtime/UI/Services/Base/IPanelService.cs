using WC.Runtime.UI;

namespace WC.Runtime.UI.Services
{
  public interface IPanelService
  {
    void Show(UIPanelID id);
    void Show(HUDPanelID id);
    void Hide(UIPanelID id);
    void Hide(HUDPanelID id);
  }
}