using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.AssetManagement;

namespace WC.Runtime.Infrastructure.Services
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerProgressData Player { get; set; }

    public void NewProgress()
    {
      Player = new PlayerProgressData(AssetName.Scene.Level.Flat1);
      
      Player.State.MaxHP = 50f;
      Player.State.ResetHP();
      
      Player.Stats.Damage = 5f;
      Player.Stats.AttackDistance = 2f;
      Player.Stats.Cooldown = 1f;
      Player.Stats.HitRadius = 2f;
      Player.Stats.MovementSpeed = 8f;
    }
  }
}