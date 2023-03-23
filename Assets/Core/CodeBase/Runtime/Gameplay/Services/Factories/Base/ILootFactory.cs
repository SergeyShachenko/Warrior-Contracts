using System.Threading.Tasks;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Loot;

namespace WC.Runtime.Gameplay.Services
{
  public interface ILootFactory : IFactory
  {
    Task<LootPiece> CreateGold();
  }
}