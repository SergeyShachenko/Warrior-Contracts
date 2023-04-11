using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ISaveLoadService
  {
    SaveLoadRegistry Registry { get; }
    
    void SaveProgress();
    void LoadProgress();
    PlayerProgressData LoadPlayerProgress();

    void CleanUp();
  }
}