namespace Infrastructure.States
{
  public interface IPayloadState<TPayload> : IStateBase
  {
    void Enter(TPayload payload);
  }
}