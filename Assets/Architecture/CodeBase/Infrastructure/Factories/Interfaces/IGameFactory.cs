using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    List<ISaverProgress> ProgressSavers { get; }
    List<ILoaderProgress> ProgressLoaders { get; }
    GameObject Player { get; }

    GameObject CreatePlayer(GameObject at);
    GameObject CreateEnemyWarrior(WarriorType warriorType, Transform parent);
    GameObject CreateHUD();
    void CleanUp();
    void Register(ILoaderProgress progressLoader);
  }
}