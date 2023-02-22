using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IGameStateMachine : IService
  {
    void Enter<TState>() where TState : class, IDefaultState;
    void Enter<TState, TParam>(TParam param) where TState : class, IPayloadState<TParam>;
  }
}