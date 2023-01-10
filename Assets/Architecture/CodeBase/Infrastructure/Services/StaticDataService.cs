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
    
    private Dictionary<WarriorType, WarriorStaticData> _enemyWarriors;
    

    public void LoadEnemyWarriors()
    {
      _enemyWarriors = Resources
        .LoadAll<WarriorStaticData>(EnemyWarriorsPath)
        .ToDictionary(x => x.Type, x => x);
    }

    public WarriorStaticData ForWarrior(WarriorType type) => 
      _enemyWarriors.TryGetValue(type, out WarriorStaticData staticData) 
        ? staticData 
        : null;
  }
}