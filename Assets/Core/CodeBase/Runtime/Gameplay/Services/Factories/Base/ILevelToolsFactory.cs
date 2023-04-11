using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Gameplay.Services
{
  public interface ILevelToolsFactory : IFactory<LevelToolsRegistry>
  {
    Task<EnemySpawnPoint> CreateEnemySpawnPoint(string spawnerID, Vector3 at, WarriorID warriorType);
  }
}