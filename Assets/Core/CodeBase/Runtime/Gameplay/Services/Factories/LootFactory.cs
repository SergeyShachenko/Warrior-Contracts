using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Loot;

namespace WC.Runtime.Gameplay.Services
{
  public class LootFactory : FactoryBase,
    ILootFactory
  {
    private readonly IPersistentProgressService _progress;

    public LootFactory(IAssetsProvider assetsProvider, IPersistentProgressService progress) : base(assetsProvider) => 
      _progress = progress;


    public async Task<LootPiece> CreateGold()
    {
      var lootObj = await p_AssetsProvider.Load<GameObject>(AssetAddress.Loot.Gold);
      
      var lootPiece = Instantiate(lootObj).GetComponent<LootPiece>();
      lootPiece.Construct(_progress.Player.World);
      
      return lootPiece; 
    }

    public override async Task WarmUp() => 
      await p_AssetsProvider.Load<GameObject>(AssetAddress.Loot.Gold);
  }
}