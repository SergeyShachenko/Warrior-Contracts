using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Services
{
  public interface ILevelToolsFactory : IFactory<LevelToolsRegistry>
  {
    Task<PlayerCamera> CreatePlayerCamera();
    Task<EnemySpawnPoint> CreateEnemySpawnPoint(string spawnerID, EnemyWarriorID warriorType, Vector3 position, Quaternion rotation);
  }
}