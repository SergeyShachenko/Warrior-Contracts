using UnityEngine;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "Progress";

    private readonly IPersistentProgressService _progressService;
    private readonly IGameFactory _gameFactory;

    public SaveLoadService(IPersistentProgressService progressService, IGameFactory gameFactory)
    {
      _progressService = progressService;
      _gameFactory = gameFactory;
    }


    public void SaveProgress()
    {
      foreach (ISaverProgress progressSaver in _gameFactory.ProgressSavers)
        progressSaver.SaveProgress(_progressService.Progress);

      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgressData LoadProgress() => 
      PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgressData>();
  }
}