using System.IO;
using WC.Runtime.Extensions;
using WC.Runtime.Infrastructure.AssetManagement;

namespace WC.Runtime.Infrastructure.Services
{
  public class ConfigService : IConfigService
  {
    public TWrapper LoadConfig<TWrapper>(string name) where TWrapper : class => File
      .ReadAllText(Path.Combine(AssetDirectory.Config.Root, name))
      .ToDeserialized<TWrapper>();
  }
}