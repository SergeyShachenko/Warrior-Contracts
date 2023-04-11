using System.Collections.Generic;

namespace WC.Runtime.Infrastructure.Services
{
  public class SaveLoadRegistry : IRegistry
  {
    public IReadOnlyList<ISaverProgress> Savers => new List<ISaverProgress>(_savers);
    public IReadOnlyList<ILoaderProgress> Loaders => new List<ILoaderProgress>(_loaders);
    
    private readonly List<ISaverProgress> _savers = new();
    private readonly List<ILoaderProgress> _loaders = new();

    
    public void Register(ISaverProgress saver) => _savers.Add(saver);
    public void Register(ILoaderProgress loader) => _loaders.Add(loader);

    public void Unregister(ISaverProgress saver) => _savers.Remove(saver);
    public void Unregister(ILoaderProgress loader) => _loaders.Remove(loader);
    
    public void CleanUp()
    {
      _savers.Clear();
      _loaders.Clear();
    }
  }
}