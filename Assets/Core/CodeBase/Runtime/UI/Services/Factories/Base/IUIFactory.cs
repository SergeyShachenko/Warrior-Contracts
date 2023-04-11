using System.Threading.Tasks;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI;

namespace WC.Runtime.UI.Services
{
  public interface IUIFactory : IFactory<UIRegistry>
  {
    Task<MainUI> CreateUI();

    Task<WindowBase> Create(UIWindowID id);
    Task<PanelBase> Create(UIPanelID id);
  }
}