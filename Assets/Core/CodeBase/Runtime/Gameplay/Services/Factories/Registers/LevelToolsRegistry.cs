using System.Collections.Generic;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Gameplay.Services
{
  public class LevelToolsRegistry : IRegistry
  {
    public IReadOnlyList<EnemySpawnPoint> EnemySpawnPoints => new List<EnemySpawnPoint>(_enemySpawnPoints);

    private readonly List<EnemySpawnPoint> _enemySpawnPoints = new();

    
    public void Register(EnemySpawnPoint spawnPoint) => _enemySpawnPoints.Add(spawnPoint);
    public void Unregister(EnemySpawnPoint spawnPoint) => _enemySpawnPoints.Remove(spawnPoint);

    public void CleanUp() => _enemySpawnPoints.Clear();
  }
}