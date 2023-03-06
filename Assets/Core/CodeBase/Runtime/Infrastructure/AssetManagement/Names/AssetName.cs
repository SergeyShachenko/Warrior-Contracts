namespace WC.Runtime.Infrastructure.AssetManagement
{
  public static class AssetName
  {
    public static ConfigName Config { get; } = new();
    public static StaticDataName StaticData { get; } = new();
    public static SceneName Scene { get; } = new();
    public static UIName UI { get; } = new();
    public static UIStaticDataName UIStaticData { get; } = new();
  }
}