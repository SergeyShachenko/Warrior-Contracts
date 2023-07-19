namespace WC.Runtime.Infrastructure.Services
{
  public interface IConfigService
  {
    TWrapper Load<TWrapper>() where TWrapper : class;
    TWrapper Load<TWrapper>(string directory) where TWrapper : class;

    void Save<TWrapper>(TWrapper wrapper) where TWrapper : class;
    void Save<TWrapper>(string directory, TWrapper wrapper) where TWrapper : class;
  }
}