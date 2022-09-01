using System;

namespace CodeBase.Data
{
  [Serializable]
  public class WorldData
  {
    public LevelPosition LevelPosition;

    public WorldData(string startLevelName)
    {
      LevelPosition = new LevelPosition(startLevelName);
    }
  }
}