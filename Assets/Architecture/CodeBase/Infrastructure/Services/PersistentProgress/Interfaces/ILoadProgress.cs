using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public interface ILoadProgress
  {
    void LoadProgress(PlayerProgress progress);
  }
}