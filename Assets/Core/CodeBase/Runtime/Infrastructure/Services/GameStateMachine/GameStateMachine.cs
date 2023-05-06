using System;
using System.Collections.Generic;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IState> _states = new();
    
    private IState _currentState;
    
    private bool _bootstrapHasOccurred;


    public void Register(IState state) => _states.Add(state.GetType(), state);
    
    public void Enter<TState>(Action onExit = null) where TState : class, IDefaultState
    {
      if (typeof(TState) != typeof(BootstrapState) && _bootstrapHasOccurred == false) return;
      
      
      SetCurrentState<TState>().Enter(onExit);
    }

    public void Enter<TState, TParam>(TParam param, Action onExit = null) where TState : class, IPayloadState<TParam>
    {
      if (typeof(TState) != typeof(BootstrapState) && _bootstrapHasOccurred == false) return;
      
      
      SetCurrentState<TState>().Enter(param, onExit);
    }

    private TState SetCurrentState<TState>() where TState : class, IState
    {
      _currentState?.Exit();
      _currentState = GetState<TState>();

      if (typeof(TState) == typeof(BootstrapState)) 
        _bootstrapHasOccurred = true;

      return (TState) _currentState;
    }

    private TState GetState<TState>() where TState : class, IState => _states[typeof(TState)] as TState;
  }
}