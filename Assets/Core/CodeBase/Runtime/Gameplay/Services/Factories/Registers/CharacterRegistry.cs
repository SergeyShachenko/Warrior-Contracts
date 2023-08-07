using System.Collections.Generic;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Services
{
  public class CharacterRegistry : IRegistry
  {
    public Player Player { get; private set; }

    public IReadOnlyList<Enemy> Enemies => new List<Enemy>(_enemies);

    private readonly List<Enemy> _enemies = new();

    
    public void Register(Player player)
    {
      Player = player;
      player.Initialized += () => OnPlayerInit(player);
    }

    public void Register(Enemy enemy)
    {
      _enemies.Add(enemy);
      enemy.Initialized += () => OnEnemyInit(enemy);
    }

    public void Unregister(Player player) => Player = null;
    public void Unregister(Enemy enemy) => _enemies.Remove(enemy);

    public void CleanUp()
    {
      Player = null;
      _enemies.Clear();
    }
    
    
    private void OnPlayerInit(Player player) => player.Death.Happened += () => Unregister(player);
    private void OnEnemyInit(Enemy enemy) => enemy.Death.Happened += () => Unregister(enemy);
  }
}