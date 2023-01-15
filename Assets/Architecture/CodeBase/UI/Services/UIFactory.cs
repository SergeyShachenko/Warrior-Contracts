﻿using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Services
{
  public class UIFactory : IUIFactory
  {
    private const string UIPath = "Prefabs/UI/UI";
    
    private readonly IAssetsProvider _assetsProvider;
    private readonly IStaticDataService _staticData;
    private readonly IPersistentProgressService _progressService;

    private Transform _ui;

    public UIFactory(
      IAssetsProvider assetsProvider, 
      IStaticDataService staticData, 
      IPersistentProgressService progressService)
    {
      _assetsProvider = assetsProvider;
      _staticData = staticData;
      _progressService = progressService;
    }


    public void CreateUI() => 
      _ui = _assetsProvider.Instantiate(UIPath).transform;

    public void CreateShop()
    {
      WindowConfig windowConfig = _staticData.ForWindow(WindowID.Shop);
      WindowBase window = Object.Instantiate(windowConfig.Window, _ui);
      window.Construct(_progressService);
    }
  }
}