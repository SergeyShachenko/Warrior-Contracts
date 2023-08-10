using System;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class PlayerData
  {
    public LifeStatsData Life;
    public MovementStatsData Movement;
    public CombatStatsData Combat;
    
    
    public PlayerData GetCopy() => new()
    {
      Life = Life.GetCopy(), 
      Movement = Movement.GetCopy(), 
      Combat = Combat.GetCopy()
    };
  }
}