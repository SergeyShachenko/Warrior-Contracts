using UnityEngine;
using UnityEngine.UI;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.UI.Elements
{
  public class StartGameButton : MonoBehaviour
  {
    [SerializeField] private StartGameType _type;
    
    [Header("Links")]
    [SerializeField] private Button _button;

    private IGameStateMachine _gameStateMachine;
    private IPersistentProgressService _progressService;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine, IPersistentProgressService progressService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;

      Init();
    }

    private void Init() => _button.onClick.AddListener(OnPressed);

    
    private void OnPressed()
    {
      if (_type == StartGameType.NewGame) 
        _progressService.NewProgress();

      _gameStateMachine.Enter<LoadSceneState, string>(_progressService.Player.World.LevelPos.LevelName);
    }
  }
}