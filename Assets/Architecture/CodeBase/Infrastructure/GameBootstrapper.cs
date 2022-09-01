using CodeBase.Infrastructure.States;
using CodeBase.Logic.Screens;
using CodeBase.Tools;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour,
    ICoroutineRunner
  {
    [SerializeField] private LoadingScreen _loadingScreenPrefab;
    
    private Game _game;

    private void Awake()
    {
      _game = new Game(this, Instantiate(_loadingScreenPrefab));
      _game.StateMachine.Enter<BootstrapState>();
      
      DontDestroyOnLoad(this);
    }
  }
}