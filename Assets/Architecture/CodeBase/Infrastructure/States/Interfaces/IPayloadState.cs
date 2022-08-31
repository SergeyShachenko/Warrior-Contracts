namespace CodeBase.Infrastructure.States
{
  public interface IPayloadState<TParam> : IStateBase
  {
    void Enter(TParam param);
  }
}