namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class CharacterStaticDataDirectory
  {
    public PlayerStaticDataDirectory Player { get; } = new();
    public EnemyStaticDataDirectory Enemy { get; } = new();
  }
}