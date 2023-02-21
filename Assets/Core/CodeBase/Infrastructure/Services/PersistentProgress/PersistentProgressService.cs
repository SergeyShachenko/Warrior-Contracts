using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerProgressData Progress { get; set; }
  }
}