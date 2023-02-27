using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Tools;
using WC.Runtime.UI.Screens;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private GameBootstrapper _gameBootstrapper;
    
    public override void InstallBindings()
    {
      Application.targetFrameRate = 60;

      BindBootstrapper();
      BindServices();
      BindGameStateMachine();
    }

    
    private void BindBootstrapper()
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
        .Bind<LoadingScreen>()
        .FromInstance(gameBootstrapper.LoadingScreen)
        .AsSingle();
    }

    private void BindServices()
    {
      Container
        .Bind<SceneLoader>()
        .FromNew()
        .AsSingle();
      
      Container
        .Bind<IAssetsProvider>()
        .To<AssetProvider>()
        .FromNew()
        .AsSingle();
      
      Container
        .Bind<IStaticDataService>()
        .To<StaticDataService>()
        .FromNew()
        .AsSingle();
      
      Container
        .Bind<IPersistentProgressService>()
        .To<PersistentProgressService>()
        .FromNew()
        .AsSingle();
      
      Container
        .Bind<IRandomService>()
        .To<RandomService>()
        .FromNew()
        .AsSingle();
      
      BindBusinessServices();
      BindUIServices();

      Container
        .Bind<ISaveLoadService>()
        .To<SaveLoadService>()
        .FromNew()
        .AsSingle();
      
      Container
        .Bind<IInputService>()
        .FromInstance(Application.isEditor ? new StandaloneInputService() : new TouchInputService())
        .AsSingle();
      
      Container
        .Bind<IGameFactory>()
        .To<GameFactory>()
        .FromNew()
        .AsSingle();
    }

    private void BindUIServices()
    {
      Container
        .Bind<IUIFactory>()
        .To<UIFactory>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IWindowService>()
        .To<WindowService>()
        .FromNew()
        .AsSingle();
    }

    private void BindBusinessServices()
    {
      Container
        .Bind<IAPProvider>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IIAPService>()
        .To<IAPService>()
        .FromNew()
        .AsSingle();
      
      Container
        .Bind<IAdsService>()
        .To<AdsService>()
        .FromNew()
        .AsSingle();
    }
    
    private void BindGameStateMachine()
    {
      Container
        .Bind<IGameStateMachine>()
        .To<GameStateMachine>()
        .FromNew()
        .AsSingle();
    }
  }
}