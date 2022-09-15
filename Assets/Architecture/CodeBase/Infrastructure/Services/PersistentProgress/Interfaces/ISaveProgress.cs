using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public interface ISaveProgress : ILoadProgress
  {
    void SaveProgress(PlayerProgress progress);
  }
}