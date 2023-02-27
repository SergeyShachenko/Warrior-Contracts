using System.Threading.Tasks;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Services
{
  public interface IUIFactory
  {
    void CreateShop();
    Task CreateUI();
  }
}