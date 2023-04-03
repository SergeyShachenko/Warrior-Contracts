using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Extensions;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.UI.Services;

namespace WC.Runtime.Infrastructure.Services
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string PlayerProgressKey = "Progress";

    private readonly IPersistentProgressService _progressService;
    private readonly IUIFactory _uiFactory;
    private readonly ICharacterFactory _characterFactory;
    private readonly ILevelFactory _levelFactory;
    private readonly ISaveLoadRegistry _registry;

    public SaveLoadService(ISaveLoadRegistry registry, IPersistentProgressService progressService)
    {
      _registry = registry;
      _progressService = progressService;
    }


    public void SaveProgress()
    {
      if (BootstrapMode.Type == BootstrapType.Debug) return;
      
      
      foreach (ISaverProgress saver in _registry.Savers)
        saver.SaveProgress(_progressService.Player);

      PlayerPrefs.SetString(PlayerProgressKey, _progressService.Player.ToJson());
      
      Debug.Log("<color=Yellow>Прогресс сохранён</color>");
    }

    public void LoadProgress()
    {
      PlayerProgressData progress = _progressService.Player.Copy();
      
      foreach (ILoaderProgress loader in _registry.Loaders)
        loader.LoadProgress(progress);
      
      Debug.Log("<color=Orange>Прогресс загружен</color>");
    }

    public PlayerProgressData LoadPlayerProgress() => 
      PlayerPrefs.GetString(PlayerProgressKey)?.ToDeserialized<PlayerProgressData>();
  }
}