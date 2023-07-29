using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;
using WC.Runtime.Extensions;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.Logic.Camera;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerMovement : CharacterMovementBase
  {
    private readonly Player _player;
    private readonly Transform _transform;
    private readonly WorldData _worldData;
    private readonly IInputService _inputService;
    private readonly PlayerCamera _camera;
    
    private Vector3 _targetLocalDirection;

    public PlayerMovement(
      Player player,
      MovementStatsData data,
      WorldData worldData,
      IInputService inputService)
      : base(player, data)
    {
      _player = player;
      _transform = player.transform;
      _worldData = worldData;
      _inputService = inputService;
      _camera = player.Camera;

      WarpToSavedPos();
    }

    
    public override void Tick()
    {
      if (IsActive == false) return;

      
      CurrentSpeed = Mathf.Lerp(CurrentSpeed, GetSpeed(), Time.deltaTime / p_Data.AccelerationTime);

      if (HasMovementInput())
      {
        Direction = GetMovementDirection();
        LocalDirection = _transform.InverseTransformDirection(Direction);
        _targetLocalDirection = LocalDirection;

        if (_inputService.UnityGetAimButton() && _inputService.UnityGetRunButton() == false)
          RotateTowardsCamera();
        else
          Rotate(Direction);
      }
      else
      {
        _targetLocalDirection = Vector3.zero;

        if (_inputService.UnityGetAimButton())
          RotateTowardsCamera();

        if (CurrentSpeed < Constants.Epsilon)
          Direction = Vector3.zero;
      }

      Direction += Physics.gravity * (CurrentSpeed > Constants.Epsilon ? 1f : 0f);
      LocalDirection = Vector3.Lerp(LocalDirection, _targetLocalDirection, Time.deltaTime / p_Data.AccelerationTime);
  
      Move(Direction);
    }



    public override void Warp(Vector3Data to)
    {
      _player.Controller.enabled = false;
      _transform.position = to.ToVector3().AddY(_player.Controller.height);
      _player.Controller.enabled = true;
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

    public override void Move(Vector3 direction) => _player.Controller.Move(direction * CurrentSpeed * Time.deltaTime);

    public override void Rotate(Vector3 direction)
    {
      float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
      Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

      _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, p_Data.TurnSmoothTime);
    }
    
    private void RotateTowardsCamera()
    {
      Vector3 targetDirection = _camera.transform.forward;
      targetDirection.y = 0f;
      float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
      Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

      _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, p_Data.TurnSmoothTime);
    }
    
    private float GetSpeed()
    {
      if (_inputService.UnityGetAimButton())
      {
        if (_inputService.UnityGetRunButton()) return p_Data.RunSpeed;

        return p_Data.SlowWalkSpeed;
      }

      if (_inputService.UnityGetRunButton()) return p_Data.RunSpeed;
      if (_inputService.UnityGetSlowWalkButton()) return p_Data.SlowWalkSpeed;

      return Mathf.Lerp(0, p_Data.WalkSpeed, Mathf.Abs(_inputService.AxisDirection.magnitude));
    }

    private Vector3 GetMovementDirection()
    {
      Vector3 targetDirection = (GetRightMovement() + GetForwardMovement()).normalized;
      targetDirection.y = 0f;

      return Vector3.Lerp(Direction, targetDirection, Time.deltaTime / p_Data.TurnSmoothTime);
    }
    
    private Vector3 GetForwardMovement()
    {
      Vector3 cameraForward = _camera.transform.forward;
      cameraForward.y = 0f;
      cameraForward.Normalize();

      return cameraForward * _inputService.AxisDirection.y;
    }

    private Vector3 GetRightMovement() => _camera.transform.right * _inputService.AxisDirection.x;

    private bool HasMovementInput() => _inputService.AxisDirection.sqrMagnitude > Constants.Epsilon;
  }
}