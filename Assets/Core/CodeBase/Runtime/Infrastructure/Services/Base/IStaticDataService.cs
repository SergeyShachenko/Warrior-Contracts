using WC.Runtime.Logic.Characters;
using WC.Runtime.StaticData;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IStaticDataService
  {
    PlayerWarriorStaticData GetPlayerWarrior(WarriorType type);
    EnemyWarriorStaticData GetEnemyWarrior(WarriorType type);
    LevelStaticData GetLevel(string sceneKey);
  }
}