namespace WC.Runtime.Infrastructure.AssetManagement
{
  public static class AssetPath
  {
    public static AssetConfigPath Config { get; }
    public static AssetStaticDataPath StaticData { get; }

    static AssetPath()
    {
      Config = new AssetConfigPath();
      StaticData = new AssetStaticDataPath();
    }
  }
}