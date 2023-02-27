using WC.Runtime.UI.Windows;
using WC.Runtime.Data;
using WC.Runtime.Logic.Characters;
using WC.Runtime.StaticData;
using WC.Runtime.UI;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IStaticDataService
  {
    PlayerWarriorStaticData ForPlayerWarrior(WarriorType type);
    EnemyWarriorStaticData ForEnemyWarrior(WarriorType type);
    LevelStaticData ForLevel(string sceneKey);
    WindowConfig ForWindow(WindowID id);
  }
}