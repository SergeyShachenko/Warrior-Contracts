using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Screens;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.UI
{
  public class MainUI : MonoBehaviour
  {
    private IGameStateMachine _gameStateMachine;
    private IPersistentProgressService _progressService;
    private IUIFactory _uiFactory;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine, IPersistentProgressService progressService, IUIFactory uiFactory)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _uiFactory = uiFactory;
    }

    
    public void Show(UIScreenID id) => _uiFactory.Registry.Screens[id].Show(smoothly: true);
    public void Show(UIWindowID id) => _uiFactory.Registry.Windows[id].Show(smoothly: true);
    public void Hide(UIScreenID id) => _uiFactory.Registry.Screens[id].Hide(smoothly: true);
    public void Hide(UIWindowID id) => _uiFactory.Registry.Windows[id].Hide(smoothly: true);

    
    public void StartGame(StartGameType type)
    {
      if (type == StartGameType.NewGame) 
        _progressService.NewProgress();

      _gameStateMachine.Enter<LoadSceneState, string>(_progressService.Player.World.LevelPos.LevelName);
    }
    
    public void GoToScene(string sceneName) => _gameStateMachine.Enter<LoadSceneState, string>(sceneName);
  }
}