using CodeBase.Infrastructure.Services.Input;
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
      IAssets assets = new AssetProvider();
      IStaticDataService staticDataService = RegisterStaticData();
      IPersistentProgressService progressService = new PersistentProgressService();
      IRandomService randomService = new RandomService();
      
      _services.RegisterSingle(InputService());
      _services.RegisterSingle(assets);
      _services.RegisterSingle(progressService);
      _services.RegisterSingle(randomService);
      _services.RegisterSingle<IGameFactory>(new GameFactory(progressService, assets, staticDataService, randomService));
      _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
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