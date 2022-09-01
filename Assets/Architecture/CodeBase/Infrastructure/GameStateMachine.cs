using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using CodeBase.Logic.Screens;

namespace CodeBase.Infrastructure
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IStateBase> _states;
    private IStateBase _currentState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices services)
    {
      _states = new Dictionary<Type, IStateBase>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, services.Single<IGameFactory>(), services.Single<IPersistentProgressService>()),
        [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),
        [typeof(GameLoopState)] = new GameLoopState(this)
      };
    }
    
    
    public void Enter<TState>() where TState : class, IDefaultState => 
      SetCurrentState<TState>().Enter();

    public void Enter<TState, TParam>(TParam param) where TState : class, IPayloadState<TParam> => 
      SetCurrentState<TState>().Enter(param);

    private TState SetCurrentState<TState>() where TState : class, IStateBase
    {
      _currentState?.Exit();
      _currentState = GetState<TState>();
      
      return (TState) _currentState;
    }

    private TState GetState<TState>() where TState : class, IStateBase => 
      _states[typeof(TState)] as TState;
  }
}