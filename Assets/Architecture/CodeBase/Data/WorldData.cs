using System;

namespace CodeBase.Data
{
  [Serializable]
  public class WorldData
  {
    public LevelPositionData LevelPos;

    public WorldData(string startLevelName)
    {
      LevelPos = new LevelPositionData(startLevelName);
    }
  }
}