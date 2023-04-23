using System.Collections.Generic;
using System.Threading.Tasks;

namespace WC.Runtime.Infrastructure.Services
{
  public class ServiceManager : IServiceManager
  {
    public IReadOnlyList<IHaveCache> HaveCacheServices => new List<IHaveCache>(_haveCacheServices);
    public IReadOnlyList<IWarmUp> WarmUpServices => new List<IWarmUp>(_warmUpServices);

    private readonly List<IHaveCache> _haveCacheServices = new();
    private readonly List<IWarmUp> _warmUpServices = new();
    
    
    public void Register(IHaveCache haveCacheService) => _haveCacheServices.Add(haveCacheService);
    public void Register(IWarmUp warmUpService) => _warmUpServices.Add(warmUpService);

    public void Unregister(IHaveCache haveCacheService) => _haveCacheServices.Remove(haveCacheService);
    public void Unregister(IWarmUp warmUpService) => _warmUpServices.Remove(warmUpService);

    public void CleanUp()
    {
      foreach (IHaveCache haveCacheService in _haveCacheServices) 
        haveCacheService.CleanUp();
    }
    
    public async Task WarmUp()
    {
      foreach (IWarmUp warmUpService in _warmUpServices) 
        await warmUpService.WarmUp();
    }
  }
}