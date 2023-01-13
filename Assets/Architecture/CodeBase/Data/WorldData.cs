using System;

namespace CodeBase.Data
{
  [Serializable]
  public class WorldData
  {
    public LevelPositionData LevelPos;
    public AllLootData AllLoot;

    public WorldData(string startLevelName)
    {
      LevelPos = new LevelPositionData(startLevelName);
      AllLoot = new AllLootData();
    }
  }
}