using System;

namespace WC.Runtime.Logic.Characters
{
  public interface ILogicComponent
  {
    event Action Changed;
    
    bool IsActive { get; set; }
    
    void Tick();
  }
}