using System;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class LootStatsData
  {
    public int Money;


    public LootStatsData GetCopy() => new()
    {
      Money = Money
    };
  }
}