using UnityEngine;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Tools
{
  public class LevelTransferTrigger : MonoBehaviour
  {
    private const string PlayerTag = "Player";
    
    [SerializeField] private string _transferTo;
    
    private IGameStateMachine _gameStateMachine;
    private bool _triggered;
    

    private void Awake() => 
      _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();

    private void OnTriggerEnter(Collider other)
    {
      if (_triggered) return;
      
      
      if (other.CompareTag(PlayerTag))
      {
        _triggered = true;
        _gameStateMachine.Enter<LoadLevelState, string>(_transferTo);
      }
    }
  }
}