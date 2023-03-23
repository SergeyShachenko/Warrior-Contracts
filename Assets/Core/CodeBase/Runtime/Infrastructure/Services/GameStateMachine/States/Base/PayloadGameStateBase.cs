using System;
using UnityEngine;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class PayloadGameStateBase<T> : IPayloadState<T>
  {
    protected readonly GameStateMachine StateMachine;
    protected readonly DiContainer Container;
    
    private Action _onExit;

    protected PayloadGameStateBase(GameStateMachine stateMachine, DiContainer container)
    {
      StateMachine = stateMachine;
      Container = container;
    }

    
    public virtual void Enter(T param, Action onExit = null)
    {
      _onExit = onExit;

      Debug.Log($"Игра перешла в {GetType().Name}");
    }

    public virtual void Exit() => 
      _onExit?.Invoke();
  }
}