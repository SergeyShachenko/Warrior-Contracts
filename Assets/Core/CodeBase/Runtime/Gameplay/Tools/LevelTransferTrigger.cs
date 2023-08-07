using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Gameplay.Tools
{
  public class LevelTransferTrigger : MonoBehaviour
  {
    [SerializeField] private string _levelName;
    
    private IGameStateMachine _gameStateMachine;
    private ISaveLoadService _saveLoadService;
    private bool _triggered;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
    {
      _gameStateMachine = gameStateMachine;
      _saveLoadService = saveLoadService;
    }


    private void OnTriggerEnter(Collider other)
    {
      if (_triggered) return;
      
      
      if (other.CompareTag(AssetTag.Player))
      {
        _triggered = true;
        _saveLoadService.SaveProgress();
        _gameStateMachine.Enter<LoadSceneState, string>(_levelName);
      }
    }
  }
}