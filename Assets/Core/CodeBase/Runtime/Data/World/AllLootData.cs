using System;

namespace WC.Runtime.Data
{
  [Serializable]
  public class AllLootData
  {
    public event Action Changed;
    
    public int Collected;

    
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

    public AllLootData GetCopy() => new()
    {
      Collected = Collected
    };
  }
}