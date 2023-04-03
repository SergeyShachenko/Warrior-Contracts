using System.Collections.Generic;
using System.Linq;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Gameplay.Services
{
  public class CharacterRegistry : ICharacterRegistry
  {
    public Player Player { get; private set; }

    private List<Enemy> _enemies = new();

    
    public void Register(Player player) => Player = player;
    public void Register(Enemy enemy) => _enemies.Add(enemy);

    public void Unregister(Player player) => Player = null;
    public void Unregister(Enemy enemy) => _enemies.Remove(enemy);

    public IEnumerable<Enemy> GetEnemies(WarriorID id) => _enemies.Where(x => x.ID == id);

    public void CleanUp()
    {
      Player = null;
      _enemies.Clear();
    }
  }
}