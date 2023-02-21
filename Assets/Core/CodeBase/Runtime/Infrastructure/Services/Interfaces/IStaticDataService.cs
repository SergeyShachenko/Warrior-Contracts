using CodeBase.Data;
using CodeBase.Data.StaticData;
using CodeBase.Logic.Characters;
using CodeBase.UI;

namespace CodeBase.Infrastructure.Services
{
  public interface IStaticDataService : IService
  {
    void LoadData();
    PlayerWarriorStaticData ForPlayerWarrior(WarriorType type);
    EnemyWarriorStaticData ForEnemyWarrior(WarriorType type);
    LevelStaticData ForLevel(string sceneKey);
    WindowConfigStaticData ForWindow(WindowID id);
  }
}