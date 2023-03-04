using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Gameplay.Services
{
  public interface ILevelFactory : IFactory
  {
    Task CreateSpawnPoint(string spawnerID, Vector3 at, WarriorType warriorType);
  }
}