using Infrastructure.Services;
using UnityEngine;

namespace Services.Input
{
  public interface IInputService : IService
  {
    Vector2 AxisDirection { get; }

    bool IsAttackButtonUp();
  }
}