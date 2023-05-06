using System.Threading.Tasks;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Services
{
  public interface IHUDFactory : IFactory<HUDRegistry>
  {
    Task<GameplayHUD> CreateHUD();
    Task<WindowBase> Create(HUDWindowID id);
    Task<ScreenBase> Create(HUDScreenID id);
  }
}