﻿using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManagement
{
  public interface IAssets : IService
  {
    void Init();
    Task<T> Load<T>(AssetReference assetRef) where T : class;
    Task<T> Load<T>(string address) where T : class;
    Task<GameObject> Instantiate(string address);
    Task<GameObject> Instantiate(string address, Vector3 at);
    void CleanUp();
  }
}