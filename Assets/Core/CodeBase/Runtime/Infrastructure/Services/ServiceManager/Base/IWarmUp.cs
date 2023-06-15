using System.Threading.Tasks;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IWarmUp
  {
    Task WarmUp();
  }
}