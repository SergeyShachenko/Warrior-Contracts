using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.UI.Data;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Gameplay.Logic;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.Infrastructure.Services
{
  public class StaticDataService : IStaticDataService
  {
    public IReadOnlyDictionary<PlayerID, PlayerStaticData> Players => new Dictionary<PlayerID, PlayerStaticData>(_players);
    public IReadOnlyDictionary<EnemyWarriorID, EnemyWarriorStaticData> EnemyWarriors => new Dictionary<EnemyWarriorID, EnemyWarriorStaticData>(_enemyWarriors);
    public IReadOnlyDictionary<string, LevelStaticData> Levels => new Dictionary<string, LevelStaticData>(_levels);

    public IReadOnlyDictionary<UIWindowID, AssetReferenceGameObject> UIWindows => new Dictionary<UIWindowID, AssetReferenceGameObject>(_uiWindows);
    public IReadOnlyDictionary<UIScreenID, AssetReferenceGameObject> UIScreens => new Dictionary<UIScreenID, AssetReferenceGameObject>(_uiScreens);
    public IReadOnlyDictionary<HUDWindowID, AssetReferenceGameObject> HUDWindows => new Dictionary<HUDWindowID, AssetReferenceGameObject>(_hudWindows);
    public IReadOnlyDictionary<HUDScreenID, AssetReferenceGameObject> HUDScreens => new Dictionary<HUDScreenID, AssetReferenceGameObject>(_hudScreens);

    private readonly Dictionary<UIWindowID, AssetReferenceGameObject> _uiWindows = new();
    private readonly Dictionary<UIScreenID, AssetReferenceGameObject> _uiScreens = new();
    private readonly Dictionary<HUDWindowID, AssetReferenceGameObject> _hudWindows = new();
    private readonly Dictionary<HUDScreenID, AssetReferenceGameObject> _hudScreens = new();
    
    private Dictionary<PlayerID, PlayerStaticData> _players;
    private Dictionary<EnemyWarriorID, EnemyWarriorStaticData> _enemyWarriors;
    private Dictionary<string, LevelStaticData> _levels;

    public StaticDataService() => LoadData();


    private void LoadData()
    {
      _players = Resources
        .LoadAll<PlayerStaticData>(AssetDirectory.StaticData.Character.Player.Warrior)
        .ToDictionary(x => x.ID, x => x);
      
      _enemyWarriors = Resources
        .LoadAll<EnemyWarriorStaticData>(AssetDirectory.StaticData.Character.Enemy.Warrior)
        .ToDictionary(x => x.ID, x => x);
      
      _levels = Resources
        .LoadAll<LevelStaticData>(AssetDirectory.StaticData.Levels)
        .ToDictionary(x => x.Name, x => x);
      
      foreach (UIWindowsStaticData data in Resources.LoadAll<UIWindowsStaticData>(AssetDirectory.StaticData.UI))
      foreach (UIWindowConfig windowConfig in data.Windows)
        _uiWindows.Add(windowConfig.ID, windowConfig.PrefabRef);
      
      foreach (UIScreensStaticData data in Resources.LoadAll<UIScreensStaticData>(AssetDirectory.StaticData.UI))
      foreach (UIScreenConfig screenConfig in data.Screens)
        _uiScreens.Add(screenConfig.ID, screenConfig.PrefabRef);
      
      foreach (HUDWindowsStaticData data in Resources.LoadAll<HUDWindowsStaticData>(AssetDirectory.StaticData.UI))
      foreach (HUDWindowConfig windowConfig in data.Windows)
        _hudWindows.Add(windowConfig.ID, windowConfig.PrefabRef);
      
      foreach (HUDScreensStaticData data in Resources.LoadAll<HUDScreensStaticData>(AssetDirectory.StaticData.UI))
      foreach (HUDScreenConfig screenConfig in data.Screens)
        _hudScreens.Add(screenConfig.ID, screenConfig.PrefabRef);
    }
  }
}