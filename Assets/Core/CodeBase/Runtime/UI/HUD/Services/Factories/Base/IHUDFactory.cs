using System.Threading.Tasks;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;
using WC.Runtime.UI;

namespace WC.Runtime.UI.Services
{
  public interface IHUDFactory : IFactory
  {
    Task<GameplayHUD> CreateHUD(Player player);
    Task<WindowBase> Create(HUDWindowID id);
    Task<PanelBase> Create(HUDPanelID id);
  }
}