using WC.Runtime.Tools;
using WC.Runtime.UI;
using UnityEngine;
using WC.Runtime.UI.Screens;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Infrastructure
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