using System.Threading.Tasks;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IFactory
  {
    Task WarmUp();
    void CleanUp();
  }
}