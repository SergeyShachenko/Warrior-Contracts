﻿using System;
using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class PayloadGameStateBase<T> : IPayloadState<T>
  {
    protected readonly IGameStateMachine p_GameStateMachine;
    
    private Action _onExit;

    protected PayloadGameStateBase(IGameStateMachine gameStateMachine)
    {
      p_GameStateMachine = gameStateMachine;
      p_GameStateMachine.Register(this);
    }


    public virtual void Enter(T param, Action onExit = null)
    {
      _onExit = onExit;
      Debug.Log($"Игра перешла в {GetType().Name}");
    }

    public virtual void Exit() => _onExit?.Invoke();
  }
}