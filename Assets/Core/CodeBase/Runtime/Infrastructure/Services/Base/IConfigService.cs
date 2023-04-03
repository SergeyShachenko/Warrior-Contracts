namespace WC.Runtime.Infrastructure.Services
{
  public interface IConfigService
  {
    TWrapper LoadConfig<TWrapper>(string name) where TWrapper : class;
  }
}