using System;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class EnemyStatsData
  {
    public LifeStatsData Life;
    public MovementStatsData Movement;
    public CombatStatsData Combat;
    public LootStatsData Loot;
    
    
    public EnemyStatsData GetCopy() => new()
    {
      Life = Life.GetCopy(), 
      Movement = Movement.GetCopy(), 
      Combat = Combat.GetCopy(),
      Loot = Loot.GetCopy()
    };
  }
}