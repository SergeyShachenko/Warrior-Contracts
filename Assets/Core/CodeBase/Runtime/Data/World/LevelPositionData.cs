using System;

namespace WC.Runtime.Data
{
  [Serializable]
  public class LevelPositionData
  {
    public string LevelName;
    public Vector3Data Position;

    public LevelPositionData(string levelName, Vector3Data position)
    {
      LevelName = levelName;
      Position = position;
    }

    public LevelPositionData(string levelName)
    {
      LevelName = levelName;
    }
  }
}