using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public interface ILoaderProgress
  {
    void LoadProgress(PlayerProgressData progressData);
  }
}