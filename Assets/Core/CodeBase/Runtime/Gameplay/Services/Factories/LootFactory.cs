using System;
using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Loot;

namespace WC.Runtime.Gameplay.Services
{
  public class LootFactory : FactoryBase<LootRegistry>,
    ILootFactory,
    IDisposable,
    IWarmUp
  {
    private readonly IAssetsProvider _assetsProvider;
    private readonly IServiceManager _serviceManager;

    public LootFactory(
      ISaveLoadService saveLoadService,
      IAssetsProvider assetsProvider,
      IServiceManager serviceManager)
      : base(saveLoadService)
    {
      _assetsProvider = assetsProvider;
      _serviceManager = serviceManager;
      
      serviceManager.Register(this);
    }


    public async Task<LootPiece> CreateGold()
    {
      GameObject lootObj = await _assetsProvider.InstantiateAsync(AssetAddress.Loot.Gold);
      RegisterProgressWatcher(lootObj);
      
      var lootPiece = lootObj.GetComponent<LootPiece>();

      Registry.Register(lootPiece);
      return lootPiece; 
    }

    async Task IWarmUp.WarmUp() => 
      await _assetsProvider.Load<GameObject>(AssetAddress.Loot.Gold);
    
    void IDisposable.Dispose() => _serviceManager.Unregister(this);
  }
}