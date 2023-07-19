using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Extensions;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerMovement : CharacterMovementBase
  {
    private readonly WorldData _worldData;
    private readonly IInputService _inputService;
    private readonly CharacterController _characterController;
    private readonly Transform _transform;

    public PlayerMovement(
      MovementStatsData data,
      WorldData worldData,
      IInputService inputService,
      CharacterController characterController,
      Transform transform)
    : base(data)
    {
      _worldData = worldData;
      _inputService = inputService;
      _characterController = characterController;
      _transform = transform;

      WarpToSavedPos();
    }
    

    public override void Tick()
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

      _characterController.Move(movementDirection * p_Data.Speed * Time.deltaTime);
    }

    
    public override void Warp(Vector3Data to)
    {
      _characterController.enabled = false;
      _transform.position = to.ToVector3().AddY(_characterController.height);
      _characterController.enabled = true;
    }

    public override void WarpToSavedPos()
    {
      if (SceneManager.GetActiveScene().name == _worldData.LevelPos.LevelName)
      {
        Vector3Data savedPos = _worldData.LevelPos.Position;

        if (savedPos != null)
          Warp(to: savedPos);
      }
    }
  }
}