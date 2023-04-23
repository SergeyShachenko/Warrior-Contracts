namespace WC.Runtime.Infrastructure.Services
{
  public interface IFactory<TRegistry> : IHaveCache, IWarmUp 
    where TRegistry : class, IRegistry
  {
    TRegistry Registry { get; }
  }
}