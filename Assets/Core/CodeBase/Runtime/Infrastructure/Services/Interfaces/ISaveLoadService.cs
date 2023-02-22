using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress();
    PlayerProgressData LoadProgress();
  }
}