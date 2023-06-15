using System.Collections.Generic;
using System.Threading.Tasks;

namespace WC.Runtime.Infrastructure.Services
{
  public class ServicesManager : IServiceManager
  {
    public IReadOnlyList<IHaveCache> HaveCacheServices => new List<IHaveCache>(_haveCacheServices);
    public IReadOnlyList<IWarmUp> WarmUpServices => new List<IWarmUp>(_warmUpServices);

    private readonly List<IHaveCache> _haveCacheServices = new();
    private readonly List<IWarmUp> _warmUpServices = new();
    
    
    public void Register(IHaveCache service) => _haveCacheServices.Add(service);
    public void Register(IWarmUp service) => _warmUpServices.Add(service);

    public void Unregister(IHaveCache service) => _haveCacheServices.Remove(service);
    public void Unregister(IWarmUp service) => _warmUpServices.Remove(service);

    public void CleanUp()
    {
      foreach (IHaveCache service in _haveCacheServices) 
        service.CleanUp();
    }
    
    public async Task WarmUp()
    {
      foreach (IWarmUp service in _warmUpServices) 
        await service.WarmUp();
    }
  }
}