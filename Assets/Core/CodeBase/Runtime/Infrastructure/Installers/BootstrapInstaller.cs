﻿using UnityEngine;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI.Elements;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller
  {
    [SerializeField] private GameBootstrapper _gameBootstrapper;
    
    public override void InstallBindings()
    {
      BindMonoServices();
      BindBaseServices();
      BindBusinessServices();
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
        .To<ServicesManager>()
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
        .To<StandaloneInputService>()
        .AsSingle();
      
      Container
        .Bind<IUIService>()
        .To<UIService>()
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
  }
}