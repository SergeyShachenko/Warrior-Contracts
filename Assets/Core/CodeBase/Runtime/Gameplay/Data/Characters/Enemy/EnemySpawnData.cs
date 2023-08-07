using System;
using UnityEngine;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class EnemySpawnData
  {
    public string ID;
    public EnemyWarriorID WarriorType;
    public Vector3 Position;
    public Quaternion Rotation;

    public EnemySpawnData(string id, EnemyWarriorID warriorType, Vector3 position, Quaternion rotation)
    {
      ID = id;
      WarriorType = warriorType;
      Position = position;
      Rotation = rotation;
    }
  }
}