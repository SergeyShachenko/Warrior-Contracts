using System;
using System.Collections.Generic;
using Architecture.Scripts.Logic.Screens;
using Infrastructure.Factories;
using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IStateBase> _states;
    private IStateBase _currentState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices services)
    {
      _states = new Dictionary<Type, IStateBase>
      {
        [typeof(BootstrapState)] = new  BootstrapState(this, sceneLoader, services),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, services.Single<IGameFactory>()),
        [typeof(GameLoopState)] = new GameLoopState(this)
      };
    }
    
    
    public void Enter<TState>() where TState : class, IDefaultState
    {
      IDefaultState nextState = ChangeState<TState>();

      nextState.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
    {
      var nextState = ChangeState<TState>();
      nextState.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IStateBase
    {
      _currentState?.Exit();
      
      var nextState = GetState<TState>();
      _currentState = nextState;
      
      return nextState;
    }

    private TState GetState<TState>() where TState : class, IStateBase => 
      _states[typeof(TState)] as TState;
  }
}