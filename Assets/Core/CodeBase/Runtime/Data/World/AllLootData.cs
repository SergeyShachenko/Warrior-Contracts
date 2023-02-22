using System;

namespace WC.Runtime.Data
{
  [Serializable]
  public class AllLootData
  {
    public int Collected;
    public Action Changed;

    public void Collect(LootData lootData)
    {
      Collected += lootData.Value;
      Changed?.Invoke();
    }

    public void Add(int loot)
    {
      Collected += loot;
      Changed?.Invoke();
    }
  }
}