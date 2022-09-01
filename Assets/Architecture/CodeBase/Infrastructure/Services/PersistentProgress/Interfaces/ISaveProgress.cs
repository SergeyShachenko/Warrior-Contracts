using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public interface IReadProgress
  {
    void ReadProgress(PlayerProgress progress);
  }

  public interface ISaveProgress : IReadProgress
  {
    void SaveProgress(PlayerProgress progress);
  }
}