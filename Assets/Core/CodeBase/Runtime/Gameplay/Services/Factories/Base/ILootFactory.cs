using System.Threading.Tasks;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Gameplay.Logic;

namespace WC.Runtime.Gameplay.Services
{
  public interface ILootFactory : IFactory<LootRegistry>
  {
    Task<LootPiece> CreateGold();
  }
}