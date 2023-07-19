using System.IO;
using WC.Runtime.Extensions;
using WC.Runtime.Infrastructure.AssetManagement;

namespace WC.Runtime.Infrastructure.Services
{
  public class ConfigService : IConfigService
  {
    public TWrapper Load<TWrapper>() where TWrapper : class => 
      Load<TWrapper>(AssetDirectory.Config.Root);

    public TWrapper Load<TWrapper>(string directory) where TWrapper : class => 
      File
        .ReadAllText(Path.Combine(directory, GenerateConfigName<TWrapper>()))
        .ToDeserialized<TWrapper>();
    
    public void Save<TWrapper>(TWrapper wrapper) where TWrapper : class => 
      Save(AssetDirectory.Config.Root, wrapper);

    public void Save<TWrapper>(string directory, TWrapper wrapper) where TWrapper : class
    {
      string path = Path.Combine(directory, GenerateConfigName<TWrapper>());
      string jsonData = wrapper.ToJson();
      File.WriteAllText(path, jsonData);
    }

    private string GenerateConfigName<TWrapper>() where TWrapper : class
    {
      return $"Config_{typeof(TWrapper).Name.Replace("Config", "").Replace("Wrapper", "")}.json";
    }
  }
}