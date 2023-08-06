using System;
using UnityEngine;

namespace WC.Runtime.Data.Characters
{
  [Serializable]
  public class PlayerSpawnData
  {
    public Vector3 Position;
    public Quaternion Rotation;

    public PlayerSpawnData(Vector3 position, Quaternion rotation)
    {
      Position = position;
      Rotation = rotation;
    }
  }
}