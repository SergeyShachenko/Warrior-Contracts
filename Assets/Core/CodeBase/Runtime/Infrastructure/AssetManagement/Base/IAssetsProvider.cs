using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Infrastructure.AssetManagement
{
  public interface IAssetsProvider
  {
    Task<T> Load<T>(AssetReference assetRef) where T : class;
    Task<T> Load<T>(string address) where T : class;
    Task<GameObject> InstantiateAsync(string address);
    Task<GameObject> InstantiateAsync(string address, Vector3 at);
    Task<GameObject> InstantiateAsync(string address, Transform under);
    void CleanUp();
  }
}