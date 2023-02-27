using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IDefaultState : IStateBase
  {
    void Enter(Action onExit = null);
  }
}