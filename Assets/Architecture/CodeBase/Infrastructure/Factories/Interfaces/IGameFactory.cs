using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    List<ISaverProgress> ProgressSavers { get; }
    List<ILoaderProgress> ProgressLoaders { get; }
    GameObject Hero { get; }

    event Action HeroCreate;

    GameObject CreateHero(GameObject at);
    GameObject CreateHUD();
    void CleanUp();
    void Register(ILoaderProgress progressLoader);
  }
}