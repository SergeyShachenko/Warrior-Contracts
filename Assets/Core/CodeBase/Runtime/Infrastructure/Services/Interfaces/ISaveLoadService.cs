using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ISaveLoadService
  {
    void SaveProgress();
    PlayerProgressData LoadProgress();
  }
}