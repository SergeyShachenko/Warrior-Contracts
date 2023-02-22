using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public class TouchInputService : InputService
  {
    public override Vector2 AxisDirection =>
      SimpleInputAxis();
  }
}