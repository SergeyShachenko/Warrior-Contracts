using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IPayloadState<TParam> : IStateBase
  {
    void Enter(TParam param, Action onExit = null);
  }
}