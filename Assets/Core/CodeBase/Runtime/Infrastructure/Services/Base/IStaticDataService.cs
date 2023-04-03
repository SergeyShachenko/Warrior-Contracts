using WC.Runtime.Logic.Characters;
using WC.Runtime.StaticData;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IStaticDataService
  {
    PlayerWarriorStaticData GetPlayerWarrior(WarriorID type);
    EnemyWarriorStaticData GetEnemyWarrior(WarriorID type);
    LevelStaticData GetLevel(string sceneKey);
  }
}