using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IPersistentProgressService : IService
  {
    PlayerProgressData Progress { get; set; }
  }
}