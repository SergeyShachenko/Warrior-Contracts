using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public interface ISaverProgress : ILoaderProgress
  {
    void SaveProgress(PlayerProgressData progressData);
  }
}