using UnityEngine;

namespace Infrastructure.AssetManagement
{
  public class AssetProvider : IAssets
  {
    public GameObject Instantiate(string path) => 
      Object.Instantiate(Resources.Load<GameObject>(path));

    public GameObject Instantiate(string path, Vector3 position) => 
      Object.Instantiate(Resources.Load<GameObject>(path), position, Quaternion.identity);
  }
}