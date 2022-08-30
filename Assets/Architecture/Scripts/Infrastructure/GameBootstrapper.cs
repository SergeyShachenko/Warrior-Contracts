using Architecture.Scripts.Logic.Screens;
using Infrastructure.States;
using Infrastructure.Tools;
using UnityEngine;

namespace Infrastructure
{
  public class GameBootstrapper : MonoBehaviour,
    ICoroutineRunner
  {
    [SerializeField] private LoadingScreen _loadingScreen;
    
    private Game _game;

    private void Awake()
    {
      _game = new Game(this, _loadingScreen);
      _game.StateMachine.Enter<BootstrapState>();
      
      DontDestroyOnLoad(this);
    }
  }
}