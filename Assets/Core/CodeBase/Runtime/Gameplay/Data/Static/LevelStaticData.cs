using System.Collections.Generic;
using UnityEngine;
using WC.Runtime.Gameplay.Data;

namespace WC.Runtime.Gameplay.Data
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Level", fileName = "Level_TYPE_NAME")]
  public class LevelStaticData : ScriptableObject
  {
    public string Name;
    public PlayerSpawnData PlayerSpawner;
    public List<EnemySpawnData> EnemySpawners;
  }
}