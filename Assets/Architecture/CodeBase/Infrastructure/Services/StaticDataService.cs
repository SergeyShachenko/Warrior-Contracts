using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.StaticData;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
  public class StaticDataService : IStaticDataService
  {
    private const string EnemyWarriorsPath = "StaticData/Characters/Enemies/Warriors";
    private const string LevelsPath = "StaticData/Levels";
    
    private Dictionary<WarriorType, WarriorStaticData> _enemyWarriors;
    private Dictionary<string, LevelStaticData> _levels;
    

    public void LoadEnemyWarriors()
    {
      _enemyWarriors = Resources
        .LoadAll<WarriorStaticData>(EnemyWarriorsPath)
        .ToDictionary(x => x.Type, x => x);
      
      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsPath)
        .ToDictionary(x => x.LevelKey, x => x);
    }

    public WarriorStaticData ForWarrior(WarriorType type) => 
      _enemyWarriors.TryGetValue(type, out WarriorStaticData staticData) 
        ? staticData 
        : null;

    public LevelStaticData ForLevel(string sceneKey) => 
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData) 
        ? staticData 
        : null;
  }
}