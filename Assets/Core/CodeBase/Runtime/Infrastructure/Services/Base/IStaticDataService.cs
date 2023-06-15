using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using WC.Runtime.Logic.Characters;
using WC.Runtime.StaticData;
using WC.Runtime.UI;
using WC.Runtime.UI.Screens;
using WC.Runtime.UI.Windows;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IStaticDataService
  {
    IReadOnlyDictionary<WarriorID, PlayerWarriorStaticData> PlayerWarriors { get; }
    IReadOnlyDictionary<WarriorID, EnemyWarriorStaticData> EnemyWarriors { get; }
    IReadOnlyDictionary<string, LevelStaticData> Levels { get; }
    
    IReadOnlyDictionary<UIWindowID, AssetReferenceGameObject> UIWindows { get; }
    IReadOnlyDictionary<UIScreenID, AssetReferenceGameObject> UIScreens { get; }
    IReadOnlyDictionary<HUDWindowID, AssetReferenceGameObject> HUDWindows { get; }
    IReadOnlyDictionary<HUDScreenID, AssetReferenceGameObject> HUDScreens { get; }
  }
}