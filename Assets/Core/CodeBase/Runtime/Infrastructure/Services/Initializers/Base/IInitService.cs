using System.Collections.Generic;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IInitService
  {
    IReadOnlyList<IInitializing> EntitiesNeedInit { get; }
    void Register(IInitializing entity);
    void Unregister(IInitializing entity);
    void DoInit();
  }
}