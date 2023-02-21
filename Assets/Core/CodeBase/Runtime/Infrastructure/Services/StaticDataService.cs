using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.StaticData;
using CodeBase.Logic.Characters;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
  public class StaticDataService : IStaticDataService
  {
    private const string EnemyWarriorsPath = "StaticData/Characters/Enemies/Warriors";
    private const string PlayerWarriorsPath = "StaticData/Characters/Players/Warriors";
    private const string LevelsPath = "StaticData/Levels";
    private const string WindowConfigsPath = "StaticData/Configs/Configs_UI_Windows";

    private Dictionary<WarriorType, PlayerWarriorStaticData> _playerWarriors;
    private Dictionary<WarriorType, EnemyWarriorStaticData> _enemyWarriors;
    private Dictionary<string, LevelStaticData> _levels;
    private Dictionary<WindowID, WindowConfigStaticData> _windowConfigs;


    public void LoadData()
    {
      _playerWarriors = Resources
        .LoadAll<PlayerWarriorStaticData>(PlayerWarriorsPath)
        .ToDictionary(x => x.Type, x => x);
      
      _enemyWarriors = Resources
        .LoadAll<EnemyWarriorStaticData>(EnemyWarriorsPath)
        .ToDictionary(x => x.Type, x => x);
      
      _levels = Resources
        .LoadAll<LevelStaticData>(LevelsPath)
        .ToDictionary(x => x.LevelKey, x => x);
      
      _windowConfigs = Resources
        .Load<WindowStaticData>(WindowConfigsPath)
        .Configs
        .ToDictionary(x => x.ID, x => x);
    }

    public PlayerWarriorStaticData ForPlayerWarrior(WarriorType type) => 
      _playerWarriors.TryGetValue(type, out PlayerWarriorStaticData staticData) 
        ? staticData
        : null;
    
    public EnemyWarriorStaticData ForEnemyWarrior(WarriorType type) => 
      _enemyWarriors.TryGetValue(type, out EnemyWarriorStaticData staticData) 
        ? staticData 
        : null;

    public LevelStaticData ForLevel(string sceneKey) => 
      _levels.TryGetValue(sceneKey, out LevelStaticData staticData) 
        ? staticData 
        : null;

    public WindowConfigStaticData ForWindow(WindowID id) => 
      _windowConfigs.TryGetValue(id, out WindowConfigStaticData windowConfig) 
        ? windowConfig 
        : null;
  }
}