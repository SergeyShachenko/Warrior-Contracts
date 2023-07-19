using System;

namespace WC.Runtime.Data.Characters
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