using System;

namespace WC.Runtime.Gameplay.Logic
{
  public interface ILogicComponent
  {
    event Action Changed;
    
    bool IsActive { get; set; }
    
    void Tick();
  }
}