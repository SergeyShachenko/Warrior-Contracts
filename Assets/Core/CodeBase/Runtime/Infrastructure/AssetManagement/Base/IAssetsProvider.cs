using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace WC.Runtime.Infrastructure.AssetManagement
{
  public interface IAssetsProvider
  {
    Task<T> Load<T>(AssetReference assetRef) where T : class;
    Task<T> Load<T>(string address) where T : class;

    GameObject Instantiate(GameObject prefab);
    GameObject Instantiate(GameObject prefab, Vector3 at);
    GameObject Instantiate(GameObject prefab, Transform under);
    GameObject Instantiate(GameObject prefab, Vector3 at, Transform under);
    
    Task<GameObject> InstantiateAsync(AssetReference assetRef);
    Task<GameObject> InstantiateAsync(AssetReference assetRef, Vector3 at);
    Task<GameObject> InstantiateAsync(AssetReference assetRef, Transform under);
    Task<GameObject> InstantiateAsync(AssetReference assetRef, Vector3 at, Transform under);
    
    Task<GameObject> InstantiateAsync(string address);
    Task<GameObject> InstantiateAsync(string address, Vector3 at);
    Task<GameObject> InstantiateAsync(string address, Transform under);
    Task<GameObject> InstantiateAsync(string address, Vector3 at, Transform under);
    
    void CleanUp();
  }
}