using UnityEngine;

namespace Services.Input
{
  public class DefaultInputService : InputService
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

    private static Vector2 UnityInputAxis() => 
      new Vector2(UnityEngine.Input.GetAxis(HorizontalAxisName), UnityEngine.Input.GetAxis(VerticalAxisName));
  }
}