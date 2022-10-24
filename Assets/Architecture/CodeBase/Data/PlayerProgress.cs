﻿using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public WorldData WorldData;
    public HeroState HeroState;
    public HeroStats HeroStats;

    public PlayerProgress(string startLevel)
    {
      WorldData = new WorldData(startLevel);
      HeroState = new HeroState();
      HeroStats = new HeroStats();
    }
  }
}