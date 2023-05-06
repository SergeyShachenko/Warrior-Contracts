namespace WC.Runtime.Infrastructure.AssetManagement
{
  public static class AssetDirectory
  {
    public static ConfigDirectory Config => new();
    public static StaticDataDirectory StaticData => new();
  }
}