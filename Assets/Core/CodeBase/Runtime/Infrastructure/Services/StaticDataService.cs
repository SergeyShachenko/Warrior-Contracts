using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Logic.Characters;
using WC.Runtime.StaticData;

namespace WC.Runtime.Infrastructure.Services
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<WarriorType, PlayerWarriorStaticData> _playerWarriors;
    private Dictionary<WarriorType, EnemyWarriorStaticData> _enemyWarriors;
    private Dictionary<string, LevelStaticData> _levels;

    public StaticDataService() => 
      LoadData();


    private void LoadData()
    {
      _playerWarriors = Resources
        .LoadAll<PlayerWarriorStaticData>(AssetPath.StaticData.PlayerWarriors)
        .ToDictionary(x => x.Type, x => x);
      
      _enemyWarriors = Resources
        .LoadAll<EnemyWarriorStaticData>(AssetPath.StaticData.EnemyWarriors)
        .ToDictionary(x => x.Type, x => x);
      
      _levels = Resources
        .LoadAll<LevelStaticData>(AssetPath.StaticData.Levels)
        .ToDictionary(x => x.LevelKey, x => x);
    }

    public PlayerWarriorStaticData GetPlayerWarrior(WarriorType type) => 
      _playerWarriors.TryGetValue(type, out PlayerWarriorStaticData staticData) 
        ? staticData
        : null;
    
    public EnemyWarriorStaticData GetEnemyWarrior(WarriorType type) => 
      _enemyWarriors.TryGetValue(type, out EnemyWarriorStaticData staticData) 
        ? staticData 
        : null;

    public LevelStaticData GetLevel(string sceneKey) => 
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData) 
        ? staticData 
        : null;
  }
}