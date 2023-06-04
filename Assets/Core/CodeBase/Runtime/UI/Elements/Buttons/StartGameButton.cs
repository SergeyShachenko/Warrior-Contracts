using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.UI.Elements
{
  public class StartGameButton : UIButtonBase
  {
    [SerializeField] private StartGameType _type;

    private IGameStateMachine _gameStateMachine;
    private IPersistentProgressService _progressService;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine, IPersistentProgressService progressService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
    }


    protected override void OnPressed()
    {
      base.OnPressed();
      
      if (_type == StartGameType.NewGame) 
        _progressService.NewProgress();

      _gameStateMachine.Enter<LoadSceneState, string>(_progressService.Player.World.LevelPos.LevelName);
    }
  }
}