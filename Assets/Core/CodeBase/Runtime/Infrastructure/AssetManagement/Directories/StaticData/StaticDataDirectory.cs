namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class StaticDataDirectory
  {
    public string Root => "StaticData/";
    public string Levels => "StaticData/Levels/";

    public CharacterStaticDataDirectory Character { get; } = new();
  }
}