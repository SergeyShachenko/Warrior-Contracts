using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WC.Runtime.Data.UI;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Logic.Characters;
using WC.Runtime.StaticData;
using WC.Runtime.UI;
using WC.Runtime.UI.Screens;
using WC.Runtime.UI.Windows;

namespace WC.Runtime.Infrastructure.Services
{
  public class StaticDataService : IStaticDataService
  {
    public IReadOnlyDictionary<WarriorID, PlayerWarriorStaticData> PlayerWarriors => new Dictionary<WarriorID, PlayerWarriorStaticData>(_playerWarriors);
    public IReadOnlyDictionary<WarriorID, EnemyWarriorStaticData> EnemyWarriors => new Dictionary<WarriorID, EnemyWarriorStaticData>(_enemyWarriors);
    public IReadOnlyDictionary<string, LevelStaticData> Levels => new Dictionary<string, LevelStaticData>(_levels);

    public IReadOnlyDictionary<UIWindowID, AssetReferenceGameObject> UIWindows => new Dictionary<UIWindowID, AssetReferenceGameObject>(_uiWindows);
    public IReadOnlyDictionary<UIScreenID, AssetReferenceGameObject> UIScreens => new Dictionary<UIScreenID, AssetReferenceGameObject>(_uiScreens);
    public IReadOnlyDictionary<HUDWindowID, AssetReferenceGameObject> HUDWindows => new Dictionary<HUDWindowID, AssetReferenceGameObject>(_hudWindows);
    public IReadOnlyDictionary<HUDScreenID, AssetReferenceGameObject> HUDScreens => new Dictionary<HUDScreenID, AssetReferenceGameObject>(_hudScreens);

    private readonly Dictionary<UIWindowID, AssetReferenceGameObject> _uiWindows = new();
    private readonly Dictionary<UIScreenID, AssetReferenceGameObject> _uiScreens = new();
    private readonly Dictionary<HUDWindowID, AssetReferenceGameObject> _hudWindows = new();
    private readonly Dictionary<HUDScreenID, AssetReferenceGameObject> _hudScreens = new();
    
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