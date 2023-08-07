using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Services
{
  public interface ICharacterFactory : IFactory<CharacterRegistry>
  {
    Task<Player> CreatePlayer(PlayerID id, PlayerSpawnData spawnData);
    Task<Enemy> CreateEnemy(EnemyWarriorID id, Transform under);
  }
}