using System;
using System.Collections.Generic;

namespace Infrastructure
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _currentState;

    public GameStateMachine(SceneLoader sceneLoader)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader)
      };
    }
    
    
    public void Enter<TState>() where TState : class, IState
    {
      IState nextState = ChangeState<TState>();

      nextState.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      var nextState = ChangeState<TState>();
      nextState.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _currentState?.Exit();
      
      var nextState = GetState<TState>();
      _currentState = nextState;
      
      return nextState;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}