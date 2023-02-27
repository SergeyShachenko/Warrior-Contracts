﻿using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.UI.Services;
using WC.Runtime.UI.Screens;
using WC.Runtime.Logic.Camera;
using WC.Runtime.Logic.Characters;
using WC.Runtime.Data.Characters;
using WC.Runtime.StaticData;
using WC.Runtime.UI.HUD;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class LoadLevelState : IPayloadState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataService _staticDataService;
    private readonly IUIFactory _uiFactory;

    private GameObject _player;
    private GameObject _ui, _hud;
    
    private Action _onExit;

    public LoadLevelState(GameStateMachine stateMachine, DiContainer container)
    {
      _stateMachine = stateMachine;
      _sceneLoader = container.Resolve<SceneLoader>();
      _loadingScreen = container.Resolve<LoadingScreen>();
      _gameFactory = container.Resolve<IGameFactory>();
      _progressService = container.Resolve<IPersistentProgressService>();
      _staticDataService = container.Resolve<IStaticDataService>();
      _uiFactory = container.Resolve<IUIFactory>();
    }


    public void Enter(string sceneName, Action onExit = null)
    {
      _onExit = onExit;      
      
      _loadingScreen.Show();
      _gameFactory.CleanUp();
      _gameFactory.WarmUp();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {
      _loadingScreen.Hide();
      _onExit?.Invoke();
    }

    private async void OnLoaded()
    {
      await CreateGameWorld();
      await CreateUI();
      _hud = await CreateHUD();

      InformProgressLoaders();

      InitHUD();
      InitCamera(_player);

      _stateMachine.Enter<GameLoopState>();
    }

    private void InitHUD()
    {
      _hud.GetComponentInChildren<ActorHUD>()
        .Construct(_player.GetComponent<Player>().Health);
    }

    private async Task CreateUI() => 
      await _uiFactory.CreateUI();

    private async Task CreateGameWorld()
    {
      LevelStaticData levelData = GetLevelStaticData();

      await CreateSpawners(levelData);
 
      // TODO Сделать механику сохранения лута
      //await InitDroppedLoot();
      
      _player = await CreatePlayer(levelData);
    }

    private async Task CreateSpawners(LevelStaticData levelData)
    {
      foreach (EnemySpawnerData spawner in levelData.EnemySpawners)
        await _gameFactory.CreateSpawnPoint(spawner.ID, spawner.Position, spawner.WarriorType);
    }

    private async Task<GameObject> CreatePlayer(LevelStaticData levelData) => 
      await _gameFactory.CreatePlayerWarrior(WarriorType.Sword, levelData.InitPlayerPos);

    // private async Task InitDroppedLoot()
    // {
    //   DroppedLoot droppedLoot = _progressService.Progress.World.DroppedLoot;
    //
    //   foreach (DroppedItem drop in droppedLoot.Items)
    //   {
    //     LootPiece loot = await _gameFactory.CreateLoot();
    //     loot.transform.position = drop.Position.AsUnityVector(); // AsUnityVector() - экстеншн
    //     loot.Init(drop.Loot);
    //   }
    // }

    private async Task<GameObject> CreateHUD() => 
      await _gameFactory.CreateHUD();

    private LevelStaticData GetLevelStaticData() => 
      _staticDataService.ForLevel(SceneManager.GetActiveScene().name);

    private void InformProgressLoaders()
    {
      foreach (ILoaderProgress progressLoader in _gameFactory.ProgressLoaders)
        progressLoader.LoadProgress(_progressService.Progress.Copy());
    }

    private void InitCamera(GameObject target) =>
      Camera.main.GetComponent<CameraMover>().Follow(target);
  }
}