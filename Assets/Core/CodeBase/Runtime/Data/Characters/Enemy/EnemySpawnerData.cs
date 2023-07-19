using System;
using UnityEngine;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Data.Characters
{
  [Serializable]
  public class EnemySpawnerData
  {
    public string ID;
    public EnemyWarriorID WarriorType;
    public Vector3 Position;

    public EnemySpawnerData(string id, EnemyWarriorID warriorType, Vector3 position)
    {
      ID = id;
      WarriorType = warriorType;
      Position = position;
    }
  }
}