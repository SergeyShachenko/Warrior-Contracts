using System.IO;
using WC.Runtime.Infrastructure.Data;
using WC.Runtime.Extensions;
using WC.Runtime.Infrastructure.AssetManagement;

namespace WC.Runtime.Infrastructure
{
  public static class BootstrapMode
  {
    public static BootstrapType Type => _config.BootstrapMode;
    public static int FPS => _config.FPS;
    
    private static BootstrapConfigWrapper _config;
    
    private static string[] _configParams;
    private static bool TypeWasChanged;

    static BootstrapMode() => LoadConfig();
    
    
    public static void SetType(BootstrapType type)
    {
#if UNITY_EDITOR
      if (TypeWasChanged) return;


      _config.BootstrapMode = type;
      TypeWasChanged = true;
#endif
    }

    private static void LoadConfig()
    {
      _config = File
        .ReadAllText(GetConfigPath())
        .ToDeserialized<BootstrapConfigWrapper>();
    }

    private static string GetConfigPath() => 
      Path.Combine(AssetDirectory.Config.Root, AssetName.Config.Bootstrap);
  }
}