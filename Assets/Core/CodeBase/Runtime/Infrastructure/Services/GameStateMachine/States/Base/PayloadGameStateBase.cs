using System;
using UnityEngine;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class PayloadGameStateBase<T> : IPayloadState<T>
  {
    protected readonly GameStateMachine p_StateMachine;
    protected readonly DiContainer p_Container;
    
    private Action _onExit;

    protected PayloadGameStateBase(GameStateMachine stateMachine, DiContainer container)
    {
      p_StateMachine = stateMachine;
      p_Container = container;
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