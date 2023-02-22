using Random = UnityEngine.Random;

namespace WC.Runtime.Infrastructure.Services
{
  public class RandomService : IRandomService
  {
    public int Next(int min, int max) =>
      Random.Range(min, max);
  }
}