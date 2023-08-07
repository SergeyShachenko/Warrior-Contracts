using System;

namespace WC.Runtime.Gameplay.Data
{
  [Serializable]
  public class WorldData
  {
    public LevelPositionData LevelPos;
    public AllLootData Loot;
    public string StartLevel;

    public WorldData(string startLevel)
    {
      LevelPos = new LevelPositionData(startLevel);
      Loot = new AllLootData();
      StartLevel = startLevel;
    }


    public WorldData GetCopy() => new(StartLevel)
    {
      LevelPos = LevelPos.GetCopy(),
      Loot = Loot.GetCopy()
    };
  }
}