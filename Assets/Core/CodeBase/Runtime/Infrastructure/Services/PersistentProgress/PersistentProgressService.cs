using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public PlayerProgressData Progress { get; set; }
  }
}