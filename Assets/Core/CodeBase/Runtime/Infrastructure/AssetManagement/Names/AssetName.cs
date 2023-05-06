namespace WC.Runtime.Infrastructure.AssetManagement
{
  public static class AssetName
  {
    public static ConfigName Config => new();
    public static StaticDataName StaticData => new();
    public static SceneName Scene => new();
    public static UIName UI => new();
    public static UIStaticDataName UIStaticData => new();
  }
}