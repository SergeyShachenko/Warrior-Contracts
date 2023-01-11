﻿using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
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
      RegisterStaticData();
      _services.RegisterSingle(InputService());
      _services.RegisterSingle<IAssets>(new AssetProvider());
      _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
      _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(), _services.Single<IStaticDataService>()));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
    }

    private void RegisterStaticData()
    {
      IStaticDataService staticData = new StaticDataService();
      staticData.LoadEnemyWarriors();
      _services.RegisterSingle(staticData);
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