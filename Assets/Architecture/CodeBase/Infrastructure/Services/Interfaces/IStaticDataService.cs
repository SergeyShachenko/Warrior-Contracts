using CodeBase.Infrastructure.StaticData;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services
{
  public interface IStaticDataService : IService
  {
    void LoadEnemyWarriors();
    WarriorStaticData ForWarrior(WarriorType type);
    LevelStaticData ForLevel(string sceneKey);
  }
}