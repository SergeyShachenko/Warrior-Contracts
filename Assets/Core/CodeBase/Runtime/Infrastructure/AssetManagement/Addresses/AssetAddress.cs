namespace WC.Runtime.Infrastructure.AssetManagement
{
  public static class AssetAddress
  {
    public static AssetUIAddress UI { get; }
    public static AssetCharacterAddress Character { get; }
    public static AssetLootAddress Loot { get; }

    public const string SpawnPoint = "SpawnPoint";
    
    static AssetAddress()
    {
      UI = new AssetUIAddress();
      Character = new AssetCharacterAddress();
      Loot = new AssetLootAddress();
    }
  }
}