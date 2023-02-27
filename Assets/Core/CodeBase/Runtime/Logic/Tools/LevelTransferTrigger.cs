using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Logic.Tools
{
  public class LevelTransferTrigger : MonoBehaviour
  {
    private const string PlayerTag = "Player";
    
    [SerializeField] private string _transferTo;
    
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
      
      
      if (other.CompareTag(PlayerTag))
      {
        _triggered = true;
        _saveLoadService.SaveProgress();
        _gameStateMachine.Enter<LoadLevelState, string>(_transferTo, OnExitLoadLevelState);
      }
    }

    private void OnExitLoadLevelState() => 
      _saveLoadService.SaveProgress();
  }
}