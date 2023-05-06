namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class CharacterStaticDataDirectory
  {
    public PlayerStaticDataDirectory Player => new();
    public EnemyStaticDataDirectory Enemy => new();
  }
}