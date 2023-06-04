using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.UI.Elements
{
  public class LoadSceneButton : UIButtonBase
  {
    [SerializeField] private string _scene;

    private IGameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine) => _gameStateMachine = gameStateMachine;

    
    protected override void OnPressed()
    {
      base.OnPressed();
      _gameStateMachine.Enter<LoadSceneState, string>(_scene);
    }
  }
}