using System;
using UnityEngine;
using WC.Runtime.Gameplay.Data;

namespace WC.Runtime.Gameplay.Logic
{
  public abstract class CharacterMovementBase : ILogicComponent
  {
    public event Action Changed;
    public event Action<MovementState> StateEnter, StateExit;

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
    
    
    public abstract void MoveToTarget(Vector3 position, MovementState state);
    public abstract void Warp(Vector3 to);
    public abstract void WarpToSavedPos();


    protected void EnterToState(MovementState state)
    {
      StateExit?.Invoke(CurrentState);
      
      CurrentState = state;
      RefreshSpeed();
      
      StateEnter?.Invoke(state);
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