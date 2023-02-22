using System;
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
  }
}