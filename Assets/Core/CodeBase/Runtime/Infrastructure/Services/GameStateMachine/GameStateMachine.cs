using System;
using System.Collections.Generic;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IStateBase> _states;
    
    private IStateBase _currentState;

    public GameStateMachine(DiContainer container)
    {
      _states = new Dictionary<Type, IStateBase>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, container),
        [typeof(LoadProgressState)] = new LoadProgressState(this, container),
        [typeof(LoadLevelState)] = new LoadLevelState(this, container),
        [typeof(GameLoopState)] = new GameLoopState(this, container)
      };
    }
    
    
    public void Enter<TState>(Action onExit = null) where TState : class, IDefaultState => 
      SetCurrentState<TState>().Enter(onExit);

    public void Enter<TState, TParam>(TParam param, Action onExit = null) where TState : class, IPayloadState<TParam> => 
      SetCurrentState<TState>().Enter(param, onExit);

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