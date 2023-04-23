using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class FactoryBase<TRegistry>
    where TRegistry : class, IRegistry, new()
  {
    public TRegistry Registry { get; } = new();
    
    protected readonly IAssetsProvider p_AssetsProvider;

    private readonly ISaveLoadService _saveLoadService;

    protected FactoryBase(IServiceManager serviceManager, IAssetsProvider assetsProvider, ISaveLoadService saveLoadService)
    {
      p_AssetsProvider = assetsProvider;
      _saveLoadService = saveLoadService;
      
      serviceManager.Register((IHaveCache)this);
      serviceManager.Register((IWarmUp)this);
    }
    

    protected void RegisterProgressWatcher(GameObject gameObject)
    {
      foreach (ILoaderProgress loader in gameObject.GetComponentsInChildren<ILoaderProgress>())
        RegisterProgressWatcher(loader);
    }

    public abstract Task WarmUp();

    public virtual void CleanUp() => Registry.CleanUp();

    private void RegisterProgressWatcher(ILoaderProgress loader)
    {
      if (loader is ISaverProgress saver) 
        _saveLoadService.Registry.Register(saver);

      _saveLoadService.Registry.Register(loader);
    }
  }
}