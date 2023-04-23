using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Loot;

namespace WC.Runtime.Gameplay.Services
{
  public class LootFactory : FactoryBase<LootRegistry>,
    ILootFactory
  {
    public LootRegistry Registry { get; } = new();
    
    public LootFactory(
      IServiceManager serviceManager,
      IAssetsProvider assetsProvider, 
      ISaveLoadService saveLoadService)
      : base(serviceManager, assetsProvider, saveLoadService)
    {
      
    }


    public async Task<LootPiece> CreateGold()
    {
      GameObject lootObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.Loot.Gold);
      RegisterProgressWatcher(lootObj);
      
      var lootPiece = lootObj.GetComponent<LootPiece>();

      Registry.Register(lootPiece);
      return lootPiece; 
    }

    public override async Task WarmUp() => 
      await p_AssetsProvider.Load<GameObject>(AssetAddress.Loot.Gold);
  }
}