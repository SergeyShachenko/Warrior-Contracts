using System;
using UnityEngine;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class EnemySpawnData
  {
    public string SpawnerID;
    public EnemyID EnemyID;
    public Vector3 Position;
    public Quaternion Rotation;

    public EnemySpawnData(string spawnerID, EnemyID enemyID, Vector3 position, Quaternion rotation)
    {
      SpawnerID = spawnerID;
      EnemyID = enemyID;
      Position = position;
      Rotation = rotation;
    }
  }
}