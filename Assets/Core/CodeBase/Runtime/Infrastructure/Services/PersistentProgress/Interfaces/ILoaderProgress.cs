using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ILoaderProgress
  {
    void LoadProgress(PlayerProgressData progressData);
  }
}