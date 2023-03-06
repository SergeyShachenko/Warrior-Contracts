namespace WC.Runtime.Infrastructure.AssetManagement
{
  public static class AssetAddress
  {
    public const string SpawnPoint = "SpawnPoint";

    public static UIAddress UI { get; } = new();
    public static CharacterAddress Character { get; } = new();
    public static LootAddress Loot { get; } = new();
  }
}