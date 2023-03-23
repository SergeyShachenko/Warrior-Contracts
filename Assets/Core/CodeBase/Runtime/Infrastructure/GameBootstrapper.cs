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

    [SerializeField] private BootstrapType _type;
    
    [Header("Links")]
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private LoadingScreen _loadingScreen;
    
    private IGameStateMachine _stateMachine;

    [Inject]
    private void Construct(IGameStateMachine stateMachine) => 
      _stateMachine = stateMachine;
    
    void IInitializable.Initialize()
    {
      DontDestroyOnLoad(this);
      _stateMachine.Enter<BootstrapState, BootstrapType>(_type);
    }
  }
}