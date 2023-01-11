using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgressData
  {
    public WorldData World;
    public PlayerStateData State;
    public PlayerStatsData Stats;
    public KillData Kill;

    public PlayerProgressData(string startLevel)
    {
      World = new WorldData(startLevel);
      State = new PlayerStateData();
      Stats = new PlayerStatsData();
      Kill = new KillData();
    }
  }
}