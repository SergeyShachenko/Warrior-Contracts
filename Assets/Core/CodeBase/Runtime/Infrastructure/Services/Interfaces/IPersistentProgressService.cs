using WC.Runtime.Data.Characters;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IPersistentProgressService
  {
    PlayerProgressData Progress { get; set; }
  }
}