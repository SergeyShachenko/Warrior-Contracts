using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using WC.Runtime.Extensions;
using Zenject;

namespace WC.Runtime.Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetsProvider
  {
    private readonly DiContainer _container;
    private readonly Dictionary<string, AsyncOperationHandle> _completedHandles = new();
    private readonly Dictionary<string, List<AsyncOperationHandle>> _resourceHandles = new();

    public AssetProvider(DiContainer container)
    {
      _container = container;
      Addressables.InitializeAsync();
    }


    public async Task<T> Load<T>(AssetReference assetRef) where T : class
    {
      if (_completedHandles.TryGetValue(assetRef.AssetGUID, out AsyncOperationHandle completedOperation))
        return completedOperation.Result as T;
      
      
      return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetRef), cacheKey: assetRef.AssetGUID);
    }

    public async Task<TAsset> Load<TAsset>(string address) where TAsset : class
    {
      if (_completedHandles.TryGetValue(address, out AsyncOperationHandle completedOperation))
        return completedOperation.Result as TAsset;
      
      
      return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<TAsset>(address), cacheKey: address);
    }

    public GameObject Instantiate(GameObject prefab) => _container.InstantiatePrefab(prefab);

    public GameObject Instantiate(GameObject prefab, Vector3 at)
    {
      GameObject gameObject = Instantiate(prefab);
      gameObject.transform.position = at;
      
      return gameObject;
    }
    
    public GameObject Instantiate(GameObject prefab, Transform under)
    {
      GameObject gameObject = Instantiate(prefab);
      gameObject.transform.SetParent(under);
      gameObject.transform.position = under.position;
      
      return gameObject;
    }
    
    public GameObject Instantiate(GameObject prefab, Vector3 at, Transform under)
    {
      GameObject gameObject = Instantiate(prefab, under);
      gameObject.transform.position = at;
      
      return gameObject;
    }

    public async Task<GameObject> InstantiateAsync(AssetReference assetRef)
    {
      var loadedAsset = await Load<GameObject>(assetRef);
      
      return Instantiate(loadedAsset);
    }

    public async Task<GameObject> InstantiateAsync(AssetReference assetRef, Vector3 at)
    {
      GameObject gameObject = await InstantiateAsync(assetRef);
      gameObject.transform.position = at;
      
      return gameObject;
    }

    public async Task<GameObject> InstantiateAsync(AssetReference assetRef, Transform under)
    {
      GameObject gameObject = await InstantiateAsync(assetRef);
      gameObject.transform.SetParent(under);
      gameObject.transform.position = under.position;
      gameObject.transform.rotation = under.rotation;
      
      return gameObject;
    }
    
    public async Task<GameObject> InstantiateAsync(AssetReference assetRef, Vector3 at, Transform under)
    {
      GameObject gameObject = await InstantiateAsync(assetRef, under);
      gameObject.transform.position = at;
      
      return gameObject;
    }
    
    public async Task<GameObject> InstantiateAsync(string address)
    {
      var loadedAsset = await Load<GameObject>(address);

      return _container.InstantiatePrefab(loadedAsset);
    }

    public async Task<GameObject> InstantiateAsync(string address, Vector3 at)
    {
      GameObject gameObject = await InstantiateAsync(address);
      gameObject.transform.position = at;
      
      return gameObject;
    }

    public async Task<GameObject> InstantiateAsync(string address, Transform under)
    {
      GameObject gameObject = await InstantiateAsync(address);
      gameObject.transform.SetParent(under);
      gameObject.transform.position = under.position;
      
      return gameObject;
    }
    
    public async Task<GameObject> InstantiateAsync(string address, Vector3 at, Transform under)
    {
      GameObject gameObject = await InstantiateAsync(address, under);
      gameObject.transform.position = at;
      
      return gameObject;
    }

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