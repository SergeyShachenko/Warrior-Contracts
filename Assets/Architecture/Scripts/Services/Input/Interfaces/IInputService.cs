using UnityEngine;

namespace Services.Input
{
  public interface IInputService
  {
    Vector2 AxisDirection { get; }

    bool IsAttackButtonUp();
  }
}