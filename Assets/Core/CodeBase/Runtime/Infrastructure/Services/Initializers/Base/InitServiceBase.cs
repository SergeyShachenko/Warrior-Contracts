using System.Collections.Generic;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class InitServiceBase
  {
    public IReadOnlyList<IInitializing> EntitiesNeedInit => new List<IInitializing>(_entitiesNeedInit);
    
    private readonly List<IInitializing> _entitiesNeedInit = new();
    private bool _bootstrapInitHappened;


    public virtual void Register(IInitializing entity)
    {
      if (_bootstrapInitHappened)
        entity.Initialize();
      else
        _entitiesNeedInit.Add(entity);
    }

    public virtual void Unregister(IInitializing entity) => _entitiesNeedInit.Remove(entity);

    public virtual void DoInit()
    {
      foreach (IInitializing entity in EntitiesNeedInit)
      {
        entity.Initialize();
        Unregister(entity);
      }

      _bootstrapInitHappened = true;
    }
  }
}