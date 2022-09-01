using CodeBase.Data;

namespace CodeBase.Infrastructure.Services
{
  public interface ISaveLoadService : IService
  {
    void SaveProgress();
    PlayerProgress LoadProgress();
  }
}