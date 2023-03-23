using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IInputService
  {
    Vector2 AxisDirection { get; }

    bool GetAttackButtonUp();
  }
}