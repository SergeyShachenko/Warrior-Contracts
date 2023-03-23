using UnityEngine;
using UnityEngine.UI;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.UI.Elements
{
  public class LoadSceneButton : MonoBehaviour
  {
    [SerializeField] private string _scene;
    
    [Header("Links")]
    [SerializeField] private Button _button;

    private IGameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
      Init();
    }

    private void Init() => 
      _button.onClick.AddListener(() => _gameStateMachine.Enter<LoadSceneState, string>(_scene));
  }
}