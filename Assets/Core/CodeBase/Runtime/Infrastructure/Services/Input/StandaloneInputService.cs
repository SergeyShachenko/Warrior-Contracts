using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
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