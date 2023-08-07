using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;
using Zenject;

namespace WC.Runtime.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour,
    IInitializable
  {
    public CoroutineRunner CoroutineRunner => _coroutineRunner;
    public LoadingScreen LoadingScreen => _loadingScreen;

    [SerializeField] private BootstrapType _type = BootstrapType.Default;
    
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
      
      BootstrapMode.SetType(_type);

      _stateMachine.Enter<BootstrapState, BootstrapConfig>(CreateBootstrapConfig());
    }

    
    private BootstrapConfig CreateBootstrapConfig() => new()
      {
        Type = _type,
        StartScene = _type == BootstrapType.Default 
          ? AssetName.Scene.MainMenu 
          : SceneManager.GetActiveScene().name
      };
  }
}