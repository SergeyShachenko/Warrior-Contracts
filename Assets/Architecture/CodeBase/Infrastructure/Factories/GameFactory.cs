using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    public List<ISaveProgress> ProgressSavers { get; } = new();
    public List<ILoadProgress> ProgressLoaders { get; } = new();
    public GameObject Hero { get; private set; }
    public event Action HeroCreate;

    private readonly IAssets _assets;

    public GameFactory(IAssets assets)
    {
      _assets = assets;
    }
    
    
    public GameObject CreateHero(GameObject at)
    {
      Hero = InstantiateRegistered(AssetPath.Character, at.transform.position);
      HeroCreate?.Invoke();
      
      return Hero;
    }

    public GameObject CreateHUD() => 
      InstantiateRegistered(AssetPath.HUD);

    public void CleanUp()
    {
      ProgressSavers.Clear();
      ProgressLoaders.Clear();
    }
    
    public void Register(ILoadProgress progressLoader)
    {
      if(progressLoader is ISaveProgress progressSaver)
        ProgressSavers.Add(progressSaver);
      
      ProgressLoaders.Add(progressLoader);
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath, position: at);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    
    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath);
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ILoadProgress progressLoader in gameObject.GetComponentsInChildren<ILoadProgress>())
        RegisterProgressWatcher(progressLoader);
    }

    private void RegisterProgressWatcher(ILoadProgress progressLoader)
    {
      if (progressLoader is ISaveProgress progressSaver) 
        ProgressSavers.Add(progressSaver);

      ProgressLoaders.Add(progressLoader);
    }
  }
}