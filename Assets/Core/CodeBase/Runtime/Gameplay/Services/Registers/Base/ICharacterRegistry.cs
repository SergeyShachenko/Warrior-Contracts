using System.Collections.Generic;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Gameplay.Services
{
  public interface ICharacterRegistry
  {
    Player Player { get; }
    
    void Register(Player player);
    void Register(Enemy enemy);
    
    void Unregister(Player player);
    void Unregister(Enemy enemy);

    IEnumerable<Enemy> GetEnemies(WarriorID id);

    void CleanUp();
  }
}