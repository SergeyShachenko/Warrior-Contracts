using System;

namespace CodeBase.Data
{
  [Serializable]
  public class LevelPosition
  {
    public string LevelName;
    public Vector3Data Position;

    public LevelPosition(string levelName, Vector3Data position)
    {
      LevelName = levelName;
      Position = position;
    }

    public LevelPosition(string levelName)
    {
      LevelName = levelName;
    }
  }
}