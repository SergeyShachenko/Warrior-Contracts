using System;
using WC.Runtime.Infrastructure.Data;
using WC.Runtime.Infrastructure.Data.IAP;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class PlayerProgressData
  {
    public PlayerID ID;
    public PlayerStatsData Stats;
    public WorldData World;
    public KillData Kill;
    public PurchaseData Purchase;

    public PlayerProgressData(PlayerID id, PlayerStatsData stats, string startLevel)
    {
      ID = id;
      Stats = stats;
      World = new WorldData(startLevel);
      Kill = new KillData();
      Purchase = new PurchaseData();
    }

    
    public PlayerProgressData GetCopy() => new(ID, Stats.GetCopy(), World.StartLevel)
    {
      World = World.GetCopy(),
      Kill = Kill.GetCopy(),
      Purchase = Purchase.GetCopy()
    };
  }
}