using System.Collections.Generic;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IServiceManager : IHaveCache, IWarmUp
  {
    IReadOnlyList<IHaveCache> HaveCacheServices { get; }
    IReadOnlyList<IWarmUp> WarmUpServices { get; }
    
    void Register(IHaveCache haveCache);
    void Register(IWarmUp warmUp);
    void Unregister(IHaveCache haveCache);
    void Unregister(IWarmUp warmUp);
  }
}