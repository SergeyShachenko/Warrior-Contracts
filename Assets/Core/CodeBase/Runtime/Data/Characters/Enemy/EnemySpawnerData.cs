using System;
using CodeBase.Logic.Characters;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
  public class EnemySpawnerData
  {
    public string ID;
    public WarriorType WarriorType;
    public Vector3 Position;

    public EnemySpawnerData(string id, WarriorType warriorType, Vector3 position)
    {
      ID = id;
      WarriorType = warriorType;
      Position = position;
    }
  }
}