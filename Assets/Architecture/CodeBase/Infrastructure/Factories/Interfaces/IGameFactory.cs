using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic.Loot;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    List<ISaverProgress> ProgressSavers { get; }
    List<ILoaderProgress> ProgressLoaders { get; }
    GameObject Player { get; }

    void Register(ILoaderProgress progressLoader);
    
    Task<GameObject> CreatePlayerWarrior(WarriorType warriorType, Vector3 at);
    Task<GameObject> CreateEnemyWarrior(WarriorType warriorType, Transform parent);
    Task CreateSpawnPoint(string spawnerID, Vector3 at, WarriorType warriorType);
    Task<LootPiece> CreateLoot();
    Task<GameObject> CreateHUD();

    Task WarmUp();
    void CleanUp();
  }
}