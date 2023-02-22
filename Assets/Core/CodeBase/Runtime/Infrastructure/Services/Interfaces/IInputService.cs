using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IInputService : IService
  {
    Vector2 AxisDirection { get; }

    bool GetAttackButtonUp();
  }
}