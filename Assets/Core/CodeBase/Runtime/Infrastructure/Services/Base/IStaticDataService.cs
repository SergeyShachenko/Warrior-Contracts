using System.Collections.Generic;
using WC.Runtime.Logic.Characters;
using WC.Runtime.StaticData;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IStaticDataService
  {
    IReadOnlyDictionary<WarriorID, PlayerWarriorStaticData> PlayerWarriors { get; }
    IReadOnlyDictionary<WarriorID, EnemyWarriorStaticData> EnemyWarriors { get; }
    IReadOnlyDictionary<string, LevelStaticData> Levels { get; }
  }
}