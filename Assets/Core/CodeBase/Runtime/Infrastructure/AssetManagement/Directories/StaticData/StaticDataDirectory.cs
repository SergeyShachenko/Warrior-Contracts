namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class StaticDataDirectory
  {
    public string Root => "StaticData/";
    public string Levels => "StaticData/Levels/";
    public string UI => "StaticData/UI/";

    public CharacterStaticDataDirectory Character => new();
  }
}