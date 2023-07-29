namespace WC.Runtime.Infrastructure.AssetManagement
{
  public static class AssetAddress
  {
    public static UIAddress UI { get; } = new();
    public static CharacterAddress Character { get; } = new();
    public static LootAddress Loot { get; } = new();
    public static ToolAddress Tool { get; } = new();
  }
}