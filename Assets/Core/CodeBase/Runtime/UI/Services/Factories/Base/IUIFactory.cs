using System.Threading.Tasks;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Screens;

namespace WC.Runtime.UI.Services
{
  public interface IUIFactory : IFactory<UIRegistry>
  {
    Task<MainUI> CreateUI();

    Task<WindowBase> Create(UIWindowID id);
    Task<ScreenBase> Create(UIScreenID id);
  }
}