using System;

namespace WC.Runtime.Data
{
  [Serializable]
  public class WorldData
  {
    public LevelPositionData LevelPos;
    public AllLootData Loot;

    public WorldData(string startLevelName)
    {
      LevelPos = new LevelPositionData(startLevelName);
      Loot = new AllLootData();
    }
  }
}