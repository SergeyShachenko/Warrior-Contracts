using UnityEngine;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.UI.Services;

namespace WC.Runtime.Infrastructure.Services
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";

    private readonly IPersistentProgressService _progressService;
    private readonly IUIFactory _uiFactory;
    private readonly ICharacterFactory _characterFactory;
    private readonly ILevelFactory _levelFactory;

    public SaveLoadService(
      IPersistentProgressService progressService,
      IUIFactory uiFactory,
      ICharacterFactory characterFactory,
      ILevelFactory levelFactory)
    {
      _progressService = progressService;
      _uiFactory = uiFactory;
      _characterFactory = characterFactory;
      _levelFactory = levelFactory;
    }


    public void SaveProgress()
    {
      Save(_uiFactory);
      Save(_characterFactory);
      Save(_levelFactory);

      PlayerPrefs.SetString(ProgressKey, _progressService.Player.ToJson());
      
      Debug.Log("<color=Yellow>Игра сохранена</color>");
    }

    public PlayerProgressData LoadProgress() => 
      PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgressData>();

    private void Save(IFactory gameFactory)
    {
      foreach (ISaverProgress progressSaver in gameFactory.ProgressSavers)
        progressSaver.SaveProgress(_progressService.Player);
    }
  }
}