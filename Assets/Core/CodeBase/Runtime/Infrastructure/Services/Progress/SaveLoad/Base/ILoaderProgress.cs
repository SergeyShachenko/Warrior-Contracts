using WC.Runtime.Gameplay.Data;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ILoaderProgress
  {
    void LoadProgress(PlayerProgressData progressData);
  }
}