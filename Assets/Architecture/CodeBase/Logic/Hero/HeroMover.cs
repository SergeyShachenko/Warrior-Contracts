using CodeBase.Data;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Logic.Hero
{
  public class HeroMover : MonoBehaviour,
    ISaveProgress
  {
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed = 8f;

    private IInputService _inputService;


    private void Awake()
    {
      _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Update()
    {
      Vector3 movementDirection = Vector3.zero;

      if (_inputService.AxisDirection.sqrMagnitude > Constants.Epsilon)
      {
        movementDirection = UnityEngine.Camera.main.transform.TransformDirection(_inputService.AxisDirection);
        movementDirection.y = 0f;
        movementDirection.Normalize();

        transform.forward = movementDirection;
      }

      movementDirection += Physics.gravity;
      
      _characterController.Move(movementDirection * _movementSpeed * Time.deltaTime);
    }

    public void SaveProgress(PlayerProgress progress)
    { 
      progress.WorldData.LevelPosition = 
        new LevelPosition(CurrentLevelName(), transform.position.ToVector3Data()); 
    }

    public void ReadProgress(PlayerProgress progress)
    {
      if (CurrentLevelName() == progress.WorldData.LevelPosition.LevelName)
      {
        Vector3Data savedPos = progress.WorldData.LevelPosition.Position;
        
        if (savedPos != null) 
          Warp(to: savedPos);
      }
    }

    private void Warp(Vector3Data to)
    {
      _characterController.enabled = false;
      transform.position = to.ToVector3().AddY(_characterController.height);
      _characterController.enabled = true;
    }

    private static string CurrentLevelName() => 
      SceneManager.GetActiveScene().name;
  }
}