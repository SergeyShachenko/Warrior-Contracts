using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public WorldData WorldData;
    public HeroState HeroState;

    public PlayerProgress(string startLevel)
    {
      WorldData = new WorldData(startLevel);
      HeroState = new HeroState();
    }
  }
}