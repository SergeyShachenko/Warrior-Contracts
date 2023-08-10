using System;
using System.Collections.Generic;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class EnemyData
  {
    public LifeStatsData Life;
    public MovementStatsData Movement;
    public CombatStatsData Combat;
    public LootStatsData Loot;
    public List<AIActionID> Actions;


    public EnemyData GetCopy() => new()
    {
      Life = Life.GetCopy(), 
      Movement = Movement.GetCopy(), 
      Combat = Combat.GetCopy(),
      Loot = Loot.GetCopy(),
      Actions = new List<AIActionID>(Actions)
    };
  }
}