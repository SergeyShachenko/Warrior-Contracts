using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
  public interface IGameStateMachine : IService
  {
    void Enter<TState>() where TState : class, IDefaultState;
    void Enter<TState, TParam>(TParam param) where TState : class, IPayloadState<TParam>;
  }
}