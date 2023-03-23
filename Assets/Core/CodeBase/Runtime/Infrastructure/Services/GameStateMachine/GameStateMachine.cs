using System;
using System.Collections.Generic;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IState> _states;

    public bool BootstrapHasOccurred { get; private set; }
    
    private IState _currentState;

    public GameStateMachine(DiContainer container)
    {
      _states = new Dictionary<Type, IState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, container),
        [typeof(LoadSceneState)] = new LoadSceneState(this, container),
        [typeof(MainMenuState)] = new MainMenuState(this, container),
        [typeof(LoadLevelState)] = new LoadLevelState(this, container),
        [typeof(GameLoopState)] = new GameLoopState(this, container)
      };
    }
    
    
    public void Enter<TState>(Action onExit = null) where TState : class, IDefaultState => 
      SetCurrentState<TState>().Enter(onExit);

    public void Enter<TState, TParam>(TParam param, Action onExit = null) where TState : class, IPayloadState<TParam> => 
      SetCurrentState<TState>().Enter(param, onExit);

    private TState SetCurrentState<TState>() where TState : class, IState
    {
      _currentState?.Exit();
      _currentState = GetState<TState>();

      if (typeof(TState) == typeof(BootstrapState)) 
        BootstrapHasOccurred = true;

      return (TState) _currentState;
    }

    private TState GetState<TState>() where TState : class, IState => 
      _states[typeof(TState)] as TState;
  }
}