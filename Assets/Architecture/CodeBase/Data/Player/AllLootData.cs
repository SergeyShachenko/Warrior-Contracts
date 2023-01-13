using System;

namespace CodeBase.Data
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
  }
}