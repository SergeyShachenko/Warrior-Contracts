using System.Collections.Generic;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IServiceManager : IHaveCache, IWarmUp
  {
    IReadOnlyList<IHaveCache> HaveCacheServices { get; }
    IReadOnlyList<IWarmUp> WarmUpServices { get; }
    
    void Register(IHaveCache service);
    void Register(IWarmUp service);
    void Unregister(IHaveCache service);
    void Unregister(IWarmUp service);
  }
}