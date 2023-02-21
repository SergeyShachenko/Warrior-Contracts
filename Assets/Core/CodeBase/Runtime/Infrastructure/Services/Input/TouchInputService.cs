using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
  public class TouchInputService : InputService
  {
    public override Vector2 AxisDirection =>
      SimpleInputAxis();
  }
}