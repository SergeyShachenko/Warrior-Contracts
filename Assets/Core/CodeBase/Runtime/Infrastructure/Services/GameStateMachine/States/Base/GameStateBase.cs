using System;
using UnityEngine;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameStateBase : IDefaultState
  {
    protected readonly GameStateMachine p_StateMachine;
    protected readonly DiContainer p_Container;
    
    private Action _onExit;

    protected GameStateBase(GameStateMachine stateMachine, DiContainer container)
    {
      p_StateMachine = stateMachine;
      p_Container = container;
    }

    
    public virtual void Enter(Action onExit = null)
    {
      _onExit = onExit;

      Debug.Log($"Игра перешла в {GetType().Name}");
    }

    public virtual void Exit() => 
      _onExit?.Invoke();
  }
}