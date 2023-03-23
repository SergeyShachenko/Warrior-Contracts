using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ISaverProgress : ILoaderProgress
  {
    void SaveProgress(PlayerProgressData progressData);
  }
}