using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetsProvider
  {
    private readonly Dictionary<string, AsyncOperationHandle> _completedHandles = new();
    private readonly Dictionary<string, List<AsyncOperationHandle>> _resourceHandles = new();

    public AssetProvider() => 
      Addressables.InitializeAsync();


    public async Task<T> Load<T>(AssetReference assetRef) where T : class
    {
      if (_completedHandles.TryGetValue(assetRef.AssetGUID, out AsyncOperationHandle completedOperation))
        return completedOperation.Result as T;

      
      return await RunWithCacheOnComplete(
        Addressables.LoadAssetAsync<T>(assetRef), 
        cacheKey: assetRef.AssetGUID);
    }

    public async Task<T> Load<T>(string address) where T : class
    {
      if (_completedHandles.TryGetValue(address, out AsyncOperationHandle completedOperation))
        return completedOperation.Result as T;
      
      
      return await RunWithCacheOnComplete(
        Addressables.LoadAssetAsync<T>(address), 
        cacheKey: address);
    }

    public Task<GameObject> InstantiateAsync(string address) => 
      Addressables.InstantiateAsync(address).Task; 

    public Task<GameObject> InstantiateAsync(string address, Vector3 at) => 
      Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;
    
    public Task<GameObject> InstantiateAsync(string address, Transform under) => 
      Addressables.InstantiateAsync(address, under).Task;

    public void CleanUp()
    {
      foreach (List<AsyncOperationHandle> resourceOperations in _resourceHandles.Values)
      foreach (AsyncOperationHandle operation in resourceOperations)
        Addressables.Release(operation);
      
      _completedHandles.Clear();
      _resourceHandles.Clear();
    }

    private void AddHandle<T>(string key, AsyncOperationHandle<T> asyncOperation) where T : class
    {
      if (_resourceHandles.TryGetValue(key, out List<AsyncOperationHandle> resourceOperations) == false)
      {
        resourceOperations = new List<AsyncOperationHandle>();
        _resourceHandles[key] = resourceOperations;
      }

      resourceOperations.Add(asyncOperation);
    }

    private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> asyncOperation, string cacheKey) where T : class
    {
      asyncOperation.Completed += x => _completedHandles[cacheKey] = x;
      AddHandle(cacheKey, asyncOperation);

      return await asyncOperation.Task;
    }
  }
}