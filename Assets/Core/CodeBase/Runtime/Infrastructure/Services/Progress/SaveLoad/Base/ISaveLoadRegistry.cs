using System.Collections.Generic;

namespace WC.Runtime.Infrastructure.Services
{
  public interface ISaveLoadRegistry
  {
    IReadOnlyList<ISaverProgress> Savers { get; }
    IReadOnlyList<ILoaderProgress> Loaders { get; }

    void Register(ISaverProgress saver);
    void Register(ILoaderProgress loader);
    
    void Unregister(ISaverProgress saver);
    void Unregister(ILoaderProgress loader);

    void CleanUp();
  }
}