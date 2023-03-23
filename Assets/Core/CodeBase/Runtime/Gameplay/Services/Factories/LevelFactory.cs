using System.Threading.Tasks;
using UnityEngine;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Infrastructure.AssetManagement;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Gameplay.Services
{
  public class LevelFactory : FactoryBase,
    ILevelFactory
  {
    private readonly IPersistentProgressService _progress;
    private readonly ICharacterFactory _characterFactory;

    public LevelFactory(
      IAssetsProvider assetsProvider,
      ISaveLoadService saveLoadService,
      IPersistentProgressService progress,
      ICharacterFactory characterFactory) 
      : base(assetsProvider, saveLoadService)
    {
      _progress = progress;
      _characterFactory = characterFactory;
    }


    public async Task CreateSpawnPoint(string spawnerID, Vector3 at, WarriorType warriorType)
    {
      var spawnerObj = await p_AssetsProvider.Load<GameObject>(AssetAddress.SpawnPoint);
      
      var spawnPoint = Instantiate(spawnerObj, at).GetComponent<EnemySpawnPoint>();
      spawnPoint.Construct(_characterFactory);
      
      spawnPoint.ID = spawnerID;
      spawnPoint.WarriorType = warriorType;
    }

    public override async Task WarmUp() => 
      await p_AssetsProvider.Load<GameObject>(AssetAddress.SpawnPoint);
  }
}