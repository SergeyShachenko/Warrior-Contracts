using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Screens;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private GameBootstrapper _gameBootstrapper;
    
    public override void InstallBindings()
    {
      Application.targetFrameRate = 60;

      BindMonoServices();
      BindBaseServices();
      BindBusinessServices();
      BindGameStateMachine();
    }

    
    private void BindMonoServices()
    {
      GameBootstrapper gameBootstrapper = Instantiate(_gameBootstrapper);

      Container
        .Bind<IInitializable>()
        .To<GameBootstrapper>()
        .FromInstance(gameBootstrapper)
        .AsSingle();

      Container
        .Bind<ICoroutineRunner>()
        .To<CoroutineRunner>()
        .FromInstance(gameBootstrapper.CoroutineRunner)
        .AsSingle();

      Container
        .Bind<ILoadingScreen>()
        .To<LoadingScreen>()
        .FromInstance(gameBootstrapper.LoadingScreen)
        .AsSingle();
    }

    private void BindBaseServices()
    {
      Container
        .Bind<IServiceManager>()
        .To<ServiceManager>()
        .AsSingle();
      
      Container
        .Bind<IConfigService>()
        .To<ConfigService>()
        .AsSingle();
      
      Container
        .Bind<IStaticDataService>()
        .To<StaticDataService>()
        .AsSingle();
      
      Container
        .Bind<IPersistentProgressService>()
        .To<PersistentProgressService>()
        .AsSingle();
      
      Container
        .Bind<ISaveLoadService>()
        .To<SaveLoadService>()
        .AsSingle();

      Container
        .Bind<ISceneLoader>()
        .To<SceneLoader>()
        .AsSingle();
      
      Container
        .Bind<IRandomService>()
        .To<RandomService>()
        .AsSingle();
      
      Container
        .Bind<IInputService>()
        .FromInstance(Application.isEditor ? new StandaloneInputService() : new TouchInputService())
        .AsSingle();
    }

    private void BindBusinessServices()
    {
      Container
        .Bind<IAPProvider>()
        .AsSingle();

      Container
        .Bind<IIAPService>()
        .To<IAPService>()
        .AsSingle();
      
      Container
        .Bind<IAdsService>()
        .To<AdsService>()
        .AsSingle();
    }

    private void BindGameStateMachine()
    {
      Container
        .Bind<IGameStateMachine>()
        .To<GameStateMachine>()
        .AsSingle();
    }
  }
}