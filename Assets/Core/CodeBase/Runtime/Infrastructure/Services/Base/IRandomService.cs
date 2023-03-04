namespace WC.Runtime.Infrastructure.Services
{
  public interface IRandomService
  {
    int Next(int minValue, int maxValue);
  }
}