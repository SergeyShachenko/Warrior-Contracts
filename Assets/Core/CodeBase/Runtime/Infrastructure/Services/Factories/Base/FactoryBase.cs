using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class FactoryBase
  {
    public List<ISaverProgress> ProgressSavers { get; } = new();
    public List<ILoaderProgress> ProgressLoaders { get; } = new();

    protected readonly IAssetsProvider p_AssetsProvider;
    
    private readonly ISaveLoadService _saveLoadService;

    protected FactoryBase(IAssetsProvider assetsProvider, ISaveLoadService saveLoadService)
    {
      p_AssetsProvider = assetsProvider;
      _saveLoadService = saveLoadService;
      
      _saveLoadService.AddSaverProgress(this);
    }


    public void Register(ILoaderProgress progressLoader)
    {
      if(progressLoader is ISaverProgress progressSaver)
        ProgressSavers.Add(progressSaver);
      
      ProgressLoaders.Add(progressLoader);
    }

    public abstract Task WarmUp();

    public virtual void CleanUp()
    {
      ProgressSavers.Clear();
      ProgressLoaders.Clear();
      p_AssetsProvider.CleanUp();
      _saveLoadService.RemoveSaverProgress(this);
    }
    
    protected GameObject Instantiate(GameObject prefab)
    {
      GameObject gameObject = Object.Instantiate(prefab);
      
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    
    protected GameObject Instantiate(GameObject prefab, Vector3 at)
    {
      GameObject gameObject = Object.Instantiate(prefab, position: at, Quaternion.identity);
      
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    protected async Task<GameObject> InstantiateAsync(string address)
    {
      GameObject gameObject = await p_AssetsProvider.InstantiateAsync(address);
      
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }
    
    protected async Task<GameObject> InstantiateAsync(string address, Transform under)
    {
      GameObject gameObject = await p_AssetsProvider.InstantiateAsync(address, under);
      
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    protected async Task<GameObject> InstantiateAsync(string address, Vector3 at)
    {
      GameObject gameObject = await p_AssetsProvider.InstantiateAsync(address, at);
      
      RegisterProgressWatchers(gameObject);
      return gameObject;
    }

    private void RegisterProgressWatchers(GameObject gameObject)
    {
      foreach (ILoaderProgress progressLoader in gameObject.GetComponentsInChildren<ILoaderProgress>())
        RegisterProgressWatcher(progressLoader);
    }

    private void RegisterProgressWatcher(ILoaderProgress progressLoader)
    {
      if (progressLoader is ISaverProgress progressSaver) 
        ProgressSavers.Add(progressSaver);

      ProgressLoaders.Add(progressLoader);
    }
  }
}