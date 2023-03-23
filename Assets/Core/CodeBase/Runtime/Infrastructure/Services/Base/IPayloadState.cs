using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IPayloadState<TParam> : IState
  {
    void Enter(TParam param, Action onExit = null);
  }
}