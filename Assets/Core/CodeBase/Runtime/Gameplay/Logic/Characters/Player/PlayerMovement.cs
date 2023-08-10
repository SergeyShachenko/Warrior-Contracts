using UnityEngine;
using UnityEngine.SceneManagement;
using WC.Runtime.Infrastructure.Data;
using WC.Runtime.Extensions;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Gameplay.Logic
{
  public class PlayerMovement : CharacterMovementBase
  {
    private readonly Player _player;
    private readonly Transform _transform;
    private readonly WorldData _worldData;
    private readonly IInputService _inputService;
    private readonly PlayerCamera _camera;

    private Vector3 _targetLocalDirection;
    private Vector3 _autoMoveTargetPos;
    private bool _isAutoMoving;

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
    }

    
    public override void Tick()
    {
      if (IsActive == false) return;

      
      if (_isAutoMoving)
        AutoMove();
      else
      {
        RefreshState();
        ManualMove();
      }
    }

    
    public override void Move(Vector3 at, MovementState state)
    {
      if (IsActive == false) return;


      EnterToState(state);
      
      _autoMoveTargetPos = at;
      _isAutoMoving = true;
    }

    public override void Warp(Vector3 to)
    {
      if (IsActive == false) return;
      
      
      _player.Controller.enabled = false;
      _transform.position = to.AddY(0.01f);
      _player.Controller.enabled = true;
    }

    public override void WarpToSavedPos()
    {
      if (IsActive == false) return;
      
      
      if (SceneManager.GetActiveScene().name == _worldData.LevelPos.LevelName)
      {
        Vector3Data savedPos = _worldData.LevelPos.Position;

        if (savedPos != null) 
          Warp(to: savedPos.ToVector3());
      }
    }
    

    private void AutoMove()
    {
      Direction = (_autoMoveTargetPos - _transform.position).normalized;
      LocalDirection = _transform.InverseTransformDirection(Direction);

      if (Vector3.Distance(_autoMoveTargetPos, _transform.position) <= Constants.Epsilon)
      {
        _isAutoMoving = false;
        Direction = Vector3.zero;
        LocalDirection = Vector3.zero;
      }
      else
        Rotate(Direction);
      
      _player.Controller.Move(Direction * CurrentSpeed * Time.deltaTime);
    }

    private void ManualMove()
    {
      if (HasMovementInput())
      {
        Direction = GetMovementDirection();
        LocalDirection = _transform.InverseTransformDirection(Direction);
        _targetLocalDirection = LocalDirection;

        if (_player.Attack.IsAiming && _inputService.UnityGetRunButton() == false)
        {
          RotateTowardsCamera();
        }
        else
        {
          Rotate(Direction);
        }
      }
      else
      {
        _targetLocalDirection = Vector3.zero;

        if (_player.Attack.IsAiming) 
          RotateTowardsCamera();

        if (CurrentSpeed < Constants.Epsilon) 
          Direction = Vector3.zero;
      }

      Direction += Physics.gravity * (CurrentSpeed > Constants.Epsilon ? 1f : 0f);
      LocalDirection = Vector3.Lerp(LocalDirection, _targetLocalDirection, Time.deltaTime / p_Data.AccelerationTime);
      
      _player.Controller.Move(Direction * CurrentSpeed * Time.deltaTime);
    }

    private void RotateTowardsCamera()
    {
      Vector3 targetDirection = _camera.transform.forward;
      targetDirection.y = 0f;
      float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
      Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

      _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, p_Data.TurnSmoothTime);
    }

    private void RefreshState()
    {
      if (HasMovementInput())
      {
        if (_player.Attack.IsAiming)
        {
          EnterToState(_inputService.UnityGetRunButton() ? MovementState.Run : MovementState.Walk);
        }
        else
        {
          if (_inputService.UnityGetRunButton())
            EnterToState(MovementState.Run);
          else
            EnterToState(_inputService.UnityGetSlowWalkButton() ? MovementState.SlowWalk : MovementState.Walk);
        }
      }
      else
      {
        EnterToState(MovementState.Idle);
      }
    }

    private Vector3 GetMovementDirection()
    {
      Vector3 targetDirection = (GetMovementByAxis(_camera.transform.right, _inputService.AxisDirection.x) +
                                 GetMovementByAxis(_camera.transform.forward, _inputService.AxisDirection.y))
        .normalized;
      
      targetDirection.y = 0f;
      return Vector3.Lerp(Direction, targetDirection, Time.deltaTime / p_Data.TurnSmoothTime);
    }

    private Vector3 GetMovementByAxis(Vector3 axis, float scale)
    {
      axis.y = 0f;
      axis.Normalize();

      return axis * scale;
    }

    private bool HasMovementInput() => _inputService.AxisDirection.sqrMagnitude > Constants.Epsilon;
  }
}