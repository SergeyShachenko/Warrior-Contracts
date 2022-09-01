﻿using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public interface IGameFactory : IService
  {
    List<ISaveProgress> ProgressSavers { get; }
    List<IReadProgress> ProgressReaders { get; }
    
    GameObject CreateHero(GameObject at);
    void CreateHUD();
    void CleanUp();
  }
}