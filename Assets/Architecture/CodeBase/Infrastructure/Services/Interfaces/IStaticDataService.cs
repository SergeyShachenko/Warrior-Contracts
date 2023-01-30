using CodeBase.Data;
using CodeBase.Infrastructure.StaticData;
using CodeBase.StaticData;
using CodeBase.UI;

namespace CodeBase.Infrastructure.Services
{
  public interface IStaticDataService : IService
  {
    void LoadData();
    PlayerWarriorStaticData ForPlayerWarrior(WarriorType type);
    EnemyWarriorStaticData ForEnemyWarrior(WarriorType type);
    LevelStaticData ForLevel(string sceneKey);
    WindowConfig ForWindow(WindowID id);
  }
}