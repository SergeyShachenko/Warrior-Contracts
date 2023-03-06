namespace WC.Runtime.Infrastructure.AssetManagement
{
  public static class AssetDirectory
  {
    public static ConfigDirectory Config { get; } = new();
    public static StaticDataDirectory StaticData { get; } = new();
  }
}