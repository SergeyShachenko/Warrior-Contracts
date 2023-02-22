using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerMover : MonoBehaviour,
    ISaverProgress
  {
    public bool IsActive
    {
      get => enabled;
      set => enabled = value;
    }

    [SerializeField] private float _movementSpeed = 8f;

    [Header("Links")]
    [SerializeField] private CharacterController _characterController;

    private IInputService _inputService;


    private void Awake()
    {
      _inputService = AllServices.Container.Single<IInputService>();
      IsActive = true;
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
    

    public void SaveProgress(PlayerProgressData progressData)
    { 
      progressData.World.LevelPos = new LevelPositionData(CurrentLevelName(), transform.position.ToVector3Data()); 
    }

    public void LoadProgress(PlayerProgressData progressData)
    {
      if (CurrentLevelName() == progressData.World.LevelPos.LevelName)
      {
        Vector3Data savedPos = progressData.World.LevelPos.Position;
        
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