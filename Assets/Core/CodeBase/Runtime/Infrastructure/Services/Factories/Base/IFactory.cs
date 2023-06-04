namespace WC.Runtime.Infrastructure.Services
{
  public interface IFactory<TRegistry> where TRegistry : class, IRegistry
  {
    TRegistry Registry { get; }
  }
}