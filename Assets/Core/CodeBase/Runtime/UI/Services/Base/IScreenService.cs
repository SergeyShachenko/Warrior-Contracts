using WC.Runtime.UI;

namespace WC.Runtime.UI.Services
{
  public interface IScreenService
  {
    void Show(UIScreenID id);
    void Show(HUDScreenID id);
    void Hide(UIScreenID id);
    void Hide(HUDScreenID id);
  }
}