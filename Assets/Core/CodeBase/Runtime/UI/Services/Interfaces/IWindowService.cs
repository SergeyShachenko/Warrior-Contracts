using WC.Runtime.UI.Windows;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Services
{
  public interface IWindowService : IService
  {
    void Open(WindowID id);
  }
}