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

    public LootFactory(
      IAssetsProvider assetsProvider, 
      ISaveLoadRegistry saveLoadRegistry,
      IPersistentProgressService progress)
      : base(assetsProvider, saveLoadRegistry)
    {
      _progress = progress;
    }


    public async Task<LootPiece> CreateGold()
    {
      GameObject lootObj = await p_AssetsProvider.InstantiateAsync(AssetAddress.Loot.Gold);
      RegisterProgressWatcher(lootObj);
      
      var lootPiece = lootObj.GetComponent<LootPiece>();

      return lootPiece; 
    }

    public override async Task WarmUp() => 
      await p_AssetsProvider.Load<GameObject>(AssetAddress.Loot.Gold);
  }
}