using System;

namespace CodeBase.Data
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