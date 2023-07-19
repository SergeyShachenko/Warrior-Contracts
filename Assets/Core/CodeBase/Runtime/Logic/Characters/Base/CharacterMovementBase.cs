using System;
using WC.Runtime.Data;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.Logic.Characters
{
  public abstract class CharacterMovementBase : ILogicComponent
  {
    public event Action Changed;
    
    public bool IsActive { get; set; } = true;

    protected readonly MovementStatsData p_Data;

    protected CharacterMovementBase(MovementStatsData data) => p_Data = data;

    
    public virtual void Tick() { }
    
    
    public abstract void Warp(Vector3Data to);
    public abstract void WarpToSavedPos();
  }
}