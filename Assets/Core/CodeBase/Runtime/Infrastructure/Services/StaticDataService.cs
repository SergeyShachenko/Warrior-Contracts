using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WC.Runtime.UI.Windows;
using WC.Runtime.Data;
using WC.Runtime.Logic.Characters;
using WC.Runtime.StaticData;
using WC.Runtime.UI;

namespace WC.Runtime.Infrastructure.Services
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
    private Dictionary<WindowID, WindowConfig> _windowConfigs;


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

    public WindowConfig ForWindow(WindowID id) => 
      _windowConfigs.TryGetValue(id, out WindowConfig windowConfig) 
        ? windowConfig 
        : null;
  }
}