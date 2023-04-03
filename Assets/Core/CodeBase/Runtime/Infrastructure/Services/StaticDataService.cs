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
    private Dictionary<WarriorID, PlayerWarriorStaticData> _playerWarriors;
    private Dictionary<WarriorID, EnemyWarriorStaticData> _enemyWarriors;
    private Dictionary<string, LevelStaticData> _levels;

    public StaticDataService() => 
      LoadData();


    private void LoadData()
    {
      _playerWarriors = Resources
        .LoadAll<PlayerWarriorStaticData>(AssetDirectory.StaticData.Character.Player.Warrior)
        .ToDictionary(x => x.Type, x => x);
      
      _enemyWarriors = Resources
        .LoadAll<EnemyWarriorStaticData>(AssetDirectory.StaticData.Character.Enemy.Warrior)
        .ToDictionary(x => x.Type, x => x);
      
      _levels = Resources
        .LoadAll<LevelStaticData>(AssetDirectory.StaticData.Levels)
        .ToDictionary(x => x.LevelKey, x => x);
    }

    public PlayerWarriorStaticData GetPlayerWarrior(WarriorID type) => 
      _playerWarriors.TryGetValue(type, out PlayerWarriorStaticData staticData) 
        ? staticData
        : null;
    
    public EnemyWarriorStaticData GetEnemyWarrior(WarriorID type) => 
      _enemyWarriors.TryGetValue(type, out EnemyWarriorStaticData staticData) 
        ? staticData 
        : null;

    public LevelStaticData GetLevel(string sceneKey) => 
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData) 
        ? staticData 
        : null;
  }
}