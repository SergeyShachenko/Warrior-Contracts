using WC.Runtime.Gameplay.Data;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IPersistentProgressService
  {
    PlayerProgressData Player { get; set; }

    void ResetProgress();
  }
}