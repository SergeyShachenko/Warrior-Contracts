using System.Collections.Generic;
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
    private readonly List<FactoryBase> _savers = new();

    public SaveLoadService(IPersistentProgressService progressService) => 
      _progressService = progressService;


    public void SaveProgress()
    {
      Save();

      PlayerPrefs.SetString(ProgressKey, _progressService.Player.ToJson());
      
      Debug.Log("<color=Yellow>Игра сохранена</color>");
    }

    public PlayerProgressData LoadPlayerProgress() => 
      PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgressData>();

    public void AddSaverProgress(FactoryBase factory) => 
      _savers.Add(factory);

    public void RemoveSaverProgress(FactoryBase factory) => 
      _savers.Remove(factory);

    private void Save()
    {
      foreach (FactoryBase saver in _savers)
      foreach (ISaverProgress progressSaver in saver.ProgressSavers)
        progressSaver.SaveProgress(_progressService.Player);
    }
  }
}