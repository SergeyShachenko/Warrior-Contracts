using System;

namespace WC.Runtime.Data.Characters
{
  [Serializable]
  public class PlayerStatsData
  {
    public LifeStatsData Life;
    public MovementStatsData Movement;
    public CombatStatsData Combat;
    
    
    public PlayerStatsData GetCopy() => new()
    {
      Life = Life.GetCopy(), 
      Movement = Movement.GetCopy(), 
      Combat = Combat.GetCopy()
    };
  }
}