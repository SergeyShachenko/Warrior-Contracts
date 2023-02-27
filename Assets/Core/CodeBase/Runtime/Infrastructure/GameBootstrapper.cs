using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Screens;
using Zenject;

namespace WC.Runtime.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour,
    IInitializable
  {
    public CoroutineRunner CoroutineRunner => _coroutineRunner;
    public LoadingScreen LoadingScreen => _loadingScreen;

    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private LoadingScreen _loadingScreen;
    
    private IGameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(IGameStateMachine gameStateMachine) => 
      _gameStateMachine = gameStateMachine;

    void IInitializable.Initialize()
    {
      _gameStateMachine.Enter<BootstrapState>();
      
      DontDestroyOnLoad(this);
    }
  }
}