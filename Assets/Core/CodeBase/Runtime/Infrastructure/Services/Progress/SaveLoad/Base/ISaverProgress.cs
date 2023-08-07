using WC.Runtime.Gameplay.Data;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ISaverProgress : ILoaderProgress
  {
    void SaveProgress(PlayerProgressData progressData);
  }
}