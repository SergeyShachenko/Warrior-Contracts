using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class FactoryBase<TRegistry>
    where TRegistry : class, IRegistry, new()
  {
    public TRegistry Registry { get; } = new();

    private readonly ISaveLoadService _saveLoadService;

    protected FactoryBase(ISaveLoadService saveLoadService) => _saveLoadService = saveLoadService;


    protected void RegisterProgressWatcher(GameObject gameObject)
    {
      foreach (ILoaderProgress loader in gameObject.GetComponentsInChildren<ILoaderProgress>())
        RegisterProgressWatcher(loader);
    }

    private void RegisterProgressWatcher(ILoaderProgress loader)
    {
      if (loader is ISaverProgress saver) 
        _saveLoadService.Registry.Register(saver);

      _saveLoadService.Registry.Register(loader);
    }
  }
}