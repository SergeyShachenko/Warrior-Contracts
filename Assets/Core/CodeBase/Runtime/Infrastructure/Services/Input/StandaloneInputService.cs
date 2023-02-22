using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public class StandaloneInputService : InputService
  {
    public override Vector2 AxisDirection
    {
      get
      {
        Vector2 axis = SimpleInputAxis();

        if (axis == Vector2.zero)
          axis = UnityInputAxis();
        
        
        return axis;
      }
    }
  }
}