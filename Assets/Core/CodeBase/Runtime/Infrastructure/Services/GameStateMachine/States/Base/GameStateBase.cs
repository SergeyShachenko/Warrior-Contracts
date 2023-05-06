using System;
using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameStateBase : IDefaultState
  {
    protected readonly IGameStateMachine p_GameStateMachine;
    
    private Action _onExit;

    public GameStateBase(IGameStateMachine gameStateMachine)
    {
      p_GameStateMachine = gameStateMachine;
      p_GameStateMachine.Register(this);
    }


    public virtual void Enter(Action onExit = null)
    {
      _onExit = onExit;
      Debug.Log($"Игра перешла в {GetType().Name}");
    }

    public virtual void Exit() => _onExit?.Invoke();
  }
}