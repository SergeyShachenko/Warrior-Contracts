using Architecture.Scripts.Logic.Camera;
using Architecture.Scripts.Logic.Screens;
using UnityEngine;

namespace Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private const string CharacterPrefPath = "Prefabs/Characters/Warriors/Heroes/Warrior_Hero_Sword";
    private const string HUDPrefPath = "Prefabs/UI/HUD/HUD";
    private const string SpawnPointNameTag = "SpawnPoint";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;

    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
    }

    
    public void Enter(string sceneName)
    {
      _loadingScreen.Show();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() => 
      _loadingScreen.Hide();

    private void OnLoaded()
    {
      GameObject spawnPoint = GameObject.FindWithTag(SpawnPointNameTag);
      
      Instantiate(HUDPrefPath);
      
      GameObject hero = Instantiate(path: CharacterPrefPath, position: spawnPoint.transform.position);
      SetCameraFollow(hero);
      
      _stateMachine.Enter<GameLoopState>();
    }
    
    private static GameObject Instantiate(string path) => 
      Object.Instantiate(Resources.Load<GameObject>(path));
    
    private static GameObject Instantiate(string path, Vector3 position) => 
      Object.Instantiate(Resources.Load<GameObject>(path), position, Quaternion.identity);

    private void SetCameraFollow(GameObject target) =>
      Camera.main.GetComponent<CameraMover>().Follow(target);
  }
}