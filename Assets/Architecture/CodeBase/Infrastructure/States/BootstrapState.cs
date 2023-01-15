﻿using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IDefaultState
  {
    private const string InitSceneName = "BootScene";
    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;
      
      RegisterServices();
    }
    

    public void Enter()
    {
      _sceneLoader.Load(InitSceneName, onLoaded: EnterLoadLevel);
    }

    public void Exit()
    {
      
    }

    private void RegisterServices()
    {
      IAssetsProvider assetsProvider = new AssetProviderProvider();
      IStaticDataService staticDataService = RegisterStaticData();
      IPersistentProgressService progressService = new PersistentProgressService();
      IRandomService randomService = new RandomService();
      IUIFactory uiFactory = new UIFactory(assetsProvider, staticDataService, progressService);
      IWindowService windowService = new WindowService(uiFactory);
      IGameFactory gameFactory = new GameFactory(progressService, assetsProvider, staticDataService, randomService, windowService);
      ISaveLoadService saveLoadService = new SaveLoadService(progressService, gameFactory);

      _services.RegisterSingle(InputService());
      _services.RegisterSingle(assetsProvider);
      _services.RegisterSingle(progressService);
      _services.RegisterSingle(randomService);
      _services.RegisterSingle(uiFactory);
      _services.RegisterSingle(windowService);
      _services.RegisterSingle(gameFactory);
      _services.RegisterSingle(saveLoadService);
    }

    private IStaticDataService RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.LoadEnemyWarriors();
      _services.RegisterSingle(staticData);

      return staticData;
    }

    private void EnterLoadLevel() => 
      _stateMachine.Enter<LoadProgressState>();

    private static IInputService InputService()
    {
      if (Application.isEditor)
        return new StandaloneInputService();
      else
        return new TouchInputService();
    }
  }
}