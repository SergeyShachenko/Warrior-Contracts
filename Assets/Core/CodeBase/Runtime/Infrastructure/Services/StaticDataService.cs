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
    public IReadOnlyDictionary<WarriorID, PlayerWarriorStaticData> PlayerWarriors => _playerWarriors;
    public IReadOnlyDictionary<WarriorID, EnemyWarriorStaticData> EnemyWarriors => _enemyWarriors;
    public IReadOnlyDictionary<string, LevelStaticData> Levels => _levels;

    private Dictionary<WarriorID, PlayerWarriorStaticData> _playerWarriors;
    private Dictionary<WarriorID, EnemyWarriorStaticData> _enemyWarriors;
    private Dictionary<string, LevelStaticData> _levels;

    public StaticDataService() => LoadData();


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
        .ToDictionary(x => x.Name, x => x);
    }
  }
}