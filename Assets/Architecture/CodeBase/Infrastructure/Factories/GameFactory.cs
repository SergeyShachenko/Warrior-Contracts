using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssets _assets;

    public GameFactory(IAssets assets)
    {
      _assets = assets;
    }
    
    
    public GameObject CreateHero(GameObject at) => 
      _assets.Instantiate(path: AssetPath.CharacterPrefPath, position: at.transform.position);

    public void CreateHUD() => 
      _assets.Instantiate(AssetPath.HUDPrefPath);
  }
}