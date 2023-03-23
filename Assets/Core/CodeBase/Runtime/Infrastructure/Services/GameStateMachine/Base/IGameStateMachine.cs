using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IGameStateMachine
  {
    bool BootstrapHasOccurred { get; }
    
    void Enter<TState>(Action onExit = null) where TState : class, IDefaultState;
    void Enter<TState, TParam>(TParam param, Action onExit = null) where TState : class, IPayloadState<TParam>;
  }
}