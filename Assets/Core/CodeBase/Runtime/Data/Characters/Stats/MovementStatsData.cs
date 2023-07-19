using System;

namespace WC.Runtime.Data.Characters
{ 
  [Serializable]
  public class MovementStatsData
  {
    public float Speed;
    public float Stamina;


    public MovementStatsData GetCopy() => new()
    {
      Speed = Speed,
      Stamina = Stamina
    };
  }
}