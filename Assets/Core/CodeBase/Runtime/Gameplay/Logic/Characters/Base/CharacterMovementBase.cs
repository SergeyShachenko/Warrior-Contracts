using System;
using UnityEngine;
using WC.Runtime.Gameplay.Data;

namespace WC.Runtime.Gameplay.Logic
{
  public abstract class CharacterMovementBase : ILogicComponent
  {
    public event Action Changed;

    public bool IsActive { get; set; } = true;

    public MovementState CurrentState { get; private set; } = MovementState.None;
    
    public float CurrentSpeed { get; protected set; }
    public Vector3 Direction { get; protected set; }
    public Vector3 LocalDirection { get; protected set; }

    public float AccelerationTime => p_Data.AccelerationTime;

    protected readonly MovementStatsData p_Data;
    
    private readonly CharacterBase _character;

    protected CharacterMovementBase(CharacterBase character, MovementStatsData data)
    {
      p_Data = data;
      _character = character;
    }


    public virtual void Tick() { }
    
    
    public abstract void Move(Vector3 at, MovementState state);
    public abstract void Warp(Vector3 to);
    public abstract void WarpToSavedPos();


    public void Rotate(Vector3 direction)
    {
      float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
      Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

      _character.transform.rotation = Quaternion.Slerp(_character.transform.rotation, targetRotation, p_Data.RunSpeed);
    }

    public void Look(Vector3 at)
    {
      Vector3 directionToLook = at - _character.transform.position;
      directionToLook.y = 0;
      Rotate(directionToLook);
    }
    
    
    protected void EnterToState(MovementState state)
    {
      CurrentState = state;
      RefreshSpeed();
    }

    private void RefreshSpeed()
    {
      switch (CurrentState)
      {
        case MovementState.Idle: CurrentSpeed = LerpSpeed(to: 0);
          break;
        case MovementState.SlowWalk: CurrentSpeed = LerpSpeed(to: p_Data.SlowWalkSpeed);
          break;
        case MovementState.Walk: CurrentSpeed = LerpSpeed(to: p_Data.WalkSpeed);
          break;
        case MovementState.Run: CurrentSpeed = LerpSpeed(to: p_Data.RunSpeed);
          break;
      }
    }

    private float LerpSpeed(float to) => Mathf.Lerp(CurrentSpeed, to, Time.deltaTime / p_Data.AccelerationTime);
  }
}