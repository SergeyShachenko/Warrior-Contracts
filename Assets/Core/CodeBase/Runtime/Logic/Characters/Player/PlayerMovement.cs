using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerMovement : IMover
  {
    public bool IsActive { get; set; } = true;

    private readonly PlayerProgressData _progressData;
    private readonly CharacterController _characterController;
    private readonly Transform _transform;
    private readonly IInputService _inputService;
    private readonly float _movementSpeed;

    public PlayerMovement(PlayerProgressData progressData, CharacterController characterController, Transform transform)
    {
      _progressData = progressData;
      _characterController = characterController;
      _transform = transform;
      _inputService = AllServices.Container.Single<IInputService>();
      _movementSpeed = _progressData.Stats.MovementSpeed;
      
      WarpToSavedPos();
    }


    public void Tick()
    {
      if (IsActive == false) return;
      
      
      Vector3 movementDirection = Vector3.zero;

      if (_inputService.AxisDirection.sqrMagnitude > Constants.Epsilon)
      {
        movementDirection = UnityEngine.Camera.main.transform.TransformDirection(_inputService.AxisDirection);
        movementDirection.y = 0f;
        movementDirection.Normalize();

        _transform.forward = movementDirection;
      }

      movementDirection += Physics.gravity;

      _characterController.Move(movementDirection * _movementSpeed * Time.deltaTime);
    }

    public void Warp(Vector3Data to)
    {
      _characterController.enabled = false;
      _transform.position = to.ToVector3().AddY(_characterController.height);
      _characterController.enabled = true;
    }

    public void WarpToSavedPos()
    {
      if (SceneManager.GetActiveScene().name == _progressData.World.LevelPos.LevelName)
      {
        Vector3Data savedPos = _progressData.World.LevelPos.Position;

        if (savedPos != null)
          Warp(to: savedPos);
      }
    }
  }
}