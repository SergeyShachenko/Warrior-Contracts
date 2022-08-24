using UnityEngine;

namespace Services.Input
{
  public class TouchInputService : InputService
  {
    public override Vector2 AxisDirection =>
      SimpleInputAxis();
  }
}