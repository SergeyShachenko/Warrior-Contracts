using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class FactoryBase
  {
    protected readonly IAssetsProvider p_AssetsProvider;

    private readonly ISaveLoadRegistry _saveLoadRegistry;

    protected FactoryBase(IAssetsProvider assetsProvider, ISaveLoadRegistry saveLoadRegistry)
    {
      p_AssetsProvider = assetsProvider;
      _saveLoadRegistry = saveLoadRegistry;
    }
    

    protected void RegisterProgressWatcher(GameObject gameObject)
    {
      foreach (ILoaderProgress loader in gameObject.GetComponentsInChildren<ILoaderProgress>())
        RegisterProgressWatcher(loader);
    }

    public abstract Task WarmUp();

    public virtual void CleanUp() => p_AssetsProvider.CleanUp();
    
    private void RegisterProgressWatcher(ILoaderProgress loader)
    {
      if (loader is ISaverProgress saver) 
        _saveLoadRegistry.Register(saver);

      _saveLoadRegistry.Register(loader);
    }
  }
}