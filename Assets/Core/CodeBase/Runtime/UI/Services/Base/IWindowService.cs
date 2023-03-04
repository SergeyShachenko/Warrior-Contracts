using WC.Runtime.UI;

namespace WC.Runtime.UI.Services
{
  public interface IWindowService
  {
    void Open(UIWindowID id);
    void Open(HUDWindowID id);
    void Close(UIWindowID id);
    void Close(HUDWindowID id);
  }
}