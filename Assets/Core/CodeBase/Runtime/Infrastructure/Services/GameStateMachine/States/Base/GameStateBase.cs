using System;
using UnityEngine;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameStateBase : IDefaultState
  {
    protected readonly GameStateMachine StateMachine;
    protected readonly DiContainer Container;
    
    private Action _onExit;

    protected GameStateBase(GameStateMachine stateMachine, DiContainer container)
    {
      StateMachine = stateMachine;
      Container = container;
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