using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Level", fileName = "Level_TYPE_NAME")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<EnemySpawnerData> EnemySpawners;
  }
}