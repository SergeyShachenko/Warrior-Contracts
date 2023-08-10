namespace WC.Runtime.Gameplay.Logic
{
  public abstract class AIActionBase
  {
    public virtual void Init() => SubscribeUpdates();
    public virtual void Dispose() => UnsubscribeUpdates();


    public virtual void Tick() { }
    
    
    protected virtual void SubscribeUpdates() { }
    protected virtual void UnsubscribeUpdates() { }
  }
}