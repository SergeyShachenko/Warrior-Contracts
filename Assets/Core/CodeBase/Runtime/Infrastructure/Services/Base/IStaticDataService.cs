using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Gameplay.Logic;
using WC.Runtime.UI.Elements;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IStaticDataService
  {
    IReadOnlyDictionary<PlayerID, PlayerStaticData> Players { get; }
    IReadOnlyDictionary<EnemyID, EnemyStaticData> Enemies { get; }
    IReadOnlyDictionary<string, LevelStaticData> Levels { get; }
    
    IReadOnlyDictionary<UIWindowID, AssetReferenceGameObject> UIWindows { get; }
    IReadOnlyDictionary<UIScreenID, AssetReferenceGameObject> UIScreens { get; }
    IReadOnlyDictionary<HUDWindowID, AssetReferenceGameObject> HUDWindows { get; }
    IReadOnlyDictionary<HUDScreenID, AssetReferenceGameObject> HUDScreens { get; }
  }
}