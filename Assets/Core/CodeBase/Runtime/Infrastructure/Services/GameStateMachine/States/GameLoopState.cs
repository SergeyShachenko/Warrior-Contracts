using System;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameLoopState : IDefaultState
  {
    private readonly GameStateMachine _stateMachine;
    
    private Action _onExit;

    public GameLoopState(GameStateMachine stateMachine, DiContainer container)
    {
      _stateMachine = stateMachine;
    }

    
    public void Enter(Action onExit = null)
    {
      _onExit = onExit;
    }

    public void Exit() => 
      _onExit?.Invoke();
  }
}