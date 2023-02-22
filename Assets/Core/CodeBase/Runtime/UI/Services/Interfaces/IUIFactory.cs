using System.Threading.Tasks;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Services
{
  public interface IUIFactory : IService
  {
    void CreateShop();
    Task CreateUI();
  }
}