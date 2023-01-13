using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public interface IPersistentProgressService : IService
  {
    PlayerProgressData Progress { get; set; }
  }
}