using System;
using UnityEngine;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterMovementBase : ILogicComponent
  {
    public event Action Changed;
    
    public bool IsActive { get; set; } = true;
    
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
    
    
    public abstract void Move(Vector3 direction);
    public abstract void Rotate(Vector3 direction);
    public abstract void Warp(Vector3Data to);
    public abstract void WarpToSavedPos();
  }
}