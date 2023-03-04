using UnityEngine;
using WC.Runtime.Gameplay.Services;
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

      BindMonoServices();
      BindBaseServices();
      BindBusinessServices();
      BindUIServices();
      BindGameplayServices();
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
        .Bind<SceneLoader>()
        .AsSingle();
      
      Container
        .Bind<IAssetsProvider>()
        .To<AssetProvider>()
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
        .Bind<IRandomService>()
        .To<RandomService>()
        .AsSingle();

      Container
        .Bind<ISaveLoadService>()
        .To<SaveLoadService>()
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

    private void BindUIServices()
    {
      Container
        .Bind<IUIRegistry>()
        .To<UIRegistry>()
        .AsSingle();
      
      Container
        .Bind<IUIFactory>()
        .To<UIFactory>()
        .AsSingle();
      
      Container
        .Bind<IHUDFactory>()
        .To<HUDFactory>()
        .AsSingle();

      Container
        .Bind<IWindowService>()
        .To<WindowService>()
        .AsSingle();
      
      Container
        .Bind<IPanelService>()
        .To<PanelService>()
        .AsSingle();
    }

    private void BindGameplayServices()
    {
      Container
        .Bind<IInputService>()
        .FromInstance(Application.isEditor ? new StandaloneInputService() : new TouchInputService())
        .AsSingle();
      
      Container
        .Bind<ILootFactory>()
        .To<LootFactory>()
        .AsSingle();

      Container
        .Bind<ICharacterFactory>()
        .To<CharacterFactory>()
        .AsSingle();

      Container
        .Bind<ILevelFactory>()
        .To<LevelFactory>()
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