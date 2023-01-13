﻿using System.Collections.Generic;
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

    GameObject CreatePlayer(GameObject at);
    GameObject CreateEnemyWarrior(WarriorType warriorType, Transform parent);
    LootPiece CreateLoot();
    GameObject CreateHUD();
    void CleanUp();
    void Register(ILoaderProgress progressLoader);
  }
}