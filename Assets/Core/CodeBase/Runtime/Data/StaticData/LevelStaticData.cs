using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Data.StaticData
{
  [CreateAssetMenu(menuName = "Dev/StaticData/Level", fileName = "Level_TYPE_NAME")]
  public class LevelStaticData : ScriptableObject
  {
    public string LevelKey;
    public List<EnemySpawnerData> EnemySpawners;
    public Vector3 InitPlayerPos;
  }
}