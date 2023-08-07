using UnityEngine;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Extensions;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.UI.Services;

namespace WC.Runtime.Infrastructure.Services
{
  public class SaveLoadService : ISaveLoadService,
    IHaveCache
  {
    public SaveLoadRegistry Registry { get; } = new();
    
    private const string PlayerProgressKey = "Progress";

    private readonly IPersistentProgressService _progressService;
    private readonly IUIFactory _uiFactory;
    private readonly ICharacterFactory _characterFactory;
    private readonly ILevelToolsFactory _levelToolsFactory;

    public SaveLoadService(IServiceManager serviceManager, IPersistentProgressService progressService)
    {
      _progressService = progressService;
      serviceManager.Register(this);
    }


    public void SaveProgress()
    {
      if (BootstrapMode.Type == BootstrapType.Debug) return;
      
      
      foreach (ISaverProgress saver in Registry.Savers)
        saver.SaveProgress(_progressService.Player);

      PlayerPrefs.SetString(PlayerProgressKey, _progressService.Player.ToJson());
      
      Debug.Log("<color=Yellow>Прогресс сохранён</color>");
    }

    public void LoadProgress()
    {
      PlayerProgressData progress = _progressService.Player.GetCopy();
      
      foreach (ILoaderProgress loader in Registry.Loaders)
        loader.LoadProgress(progress);
      
      Debug.Log("<color=Orange>Прогресс загружен</color>");
    }

    public PlayerProgressData LoadPlayerProgress() => 
      PlayerPrefs.GetString(PlayerProgressKey)?.ToDeserialized<PlayerProgressData>();

    public void CleanUp() => Registry.CleanUp();
  }
}