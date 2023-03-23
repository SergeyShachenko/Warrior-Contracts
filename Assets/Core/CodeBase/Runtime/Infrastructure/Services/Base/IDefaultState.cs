using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IDefaultState : IState
  {
    void Enter(Action onExit = null);
  }
}