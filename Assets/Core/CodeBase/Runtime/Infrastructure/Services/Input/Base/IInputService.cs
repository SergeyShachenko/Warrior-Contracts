using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IInputService
  {
    Vector2 AxisDirection { get; }
    Vector2 LookDelta { get; }

    bool SimpleInputGetAttackButtonUp();
    bool UnityGetAttackButton();
    bool UnityGetRunButton();
    bool UnityGetSlowWalkButton();
    bool UnityGetAimButton();
  }
}