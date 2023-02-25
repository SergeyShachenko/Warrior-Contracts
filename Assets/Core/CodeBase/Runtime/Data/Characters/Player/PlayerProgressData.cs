using System;
using System.Linq;
using WC.Runtime.Data.IAP;

namespace WC.Runtime.Data.Characters
{
  [Serializable]
  public class PlayerProgressData
  {
    public WorldData World;
    public PlayerStateData State;
    public PlayerStatsData Stats;
    public KillData Kill;
    public PurchaseData Purchase;

    public PlayerProgressData(string startLevel)
    {
      World = new WorldData(startLevel);
      State = new PlayerStateData();
      Stats = new PlayerStatsData();
      Kill = new KillData();
      Purchase = new PurchaseData();
    }

    
    public PlayerProgressData Copy()
    {
      var copy = new PlayerProgressData(World.StartLevel);
      copy.World.Loot.Collected = World.Loot.Collected;
      copy.World.LevelPos = World.LevelPos;

      copy.State.CurrentHP = State.CurrentHP;
      copy.State.MaxHP = State.MaxHP;
      
      copy.Stats.Cooldown = Stats.Cooldown;
      copy.Stats.Damage = Stats.Damage;
      copy.Stats.AttackDistance = Stats.AttackDistance;
      copy.Stats.HitRadius = Stats.HitRadius;
      copy.Stats.MovementSpeed = Stats.MovementSpeed;
      
      copy.Kill.ClearedSpawners = Kill.ClearedSpawners.ToList();
      copy.Purchase = Purchase.Copy();

      return copy;
    }
  }
}