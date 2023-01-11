using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
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
        progressSaver.SaveProgress(_progressService.ProgressData);

      PlayerPrefs.SetString(ProgressKey, _progressService.ProgressData.ToJson());
    }

    public PlayerProgressData LoadProgress() => 
      PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgressData>();
  }
}