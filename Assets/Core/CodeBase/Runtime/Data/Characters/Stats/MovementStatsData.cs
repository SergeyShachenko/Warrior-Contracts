using System;

namespace WC.Runtime.Data.Characters
{ 
  [Serializable]
  public class MovementStatsData
  {
    public float SlowWalkSpeed;
    public float WalkSpeed;
    public float RunSpeed;
    
    public float AccelerationTime;
    public float TurnSmoothTime;

    public float Stamina;


    public MovementStatsData GetCopy() => new()
    {
      SlowWalkSpeed = SlowWalkSpeed,
      WalkSpeed = WalkSpeed,
      RunSpeed = RunSpeed,
      
      AccelerationTime = AccelerationTime,
      TurnSmoothTime = TurnSmoothTime,
      
      Stamina = Stamina
    };
  }
}