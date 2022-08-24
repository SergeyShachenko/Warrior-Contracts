using UnityEngine;

namespace Services.Input
{
  public abstract class InputService : IInputService
  {
    public abstract Vector2 AxisDirection { get; }

    protected const string VerticalAxisName = "Vertical";
    protected const string HorizontalAxisName = "Horizontal";
    private const string AttackButtonName = "Attack";


    public bool IsAttackButtonUp() => 
      SimpleInput.GetButtonUp(AttackButtonName);

    protected static Vector2 SimpleInputAxis() => 
      new Vector2(SimpleInput.GetAxis(HorizontalAxisName), SimpleInput.GetAxis(VerticalAxisName));
  }
}