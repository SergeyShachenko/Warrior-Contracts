using System;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class LootData
  {
    public int Value;
    
    public LootData(int value)
    {
      Value = value;
    }
  }
}