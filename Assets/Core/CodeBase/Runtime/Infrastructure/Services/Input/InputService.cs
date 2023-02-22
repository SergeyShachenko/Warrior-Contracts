using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class InputService : IInputService
  {
    public abstract Vector2 AxisDirection { get; }

    private const string VerticalAxisName = "Vertical";
    private const string HorizontalAxisName = "Horizontal";
    private const string AttackButtonName = "Attack";


    public bool GetAttackButtonUp() => 
      SimpleInput.GetButtonUp(AttackButtonName);

    protected static Vector2 SimpleInputAxis() => 
      new Vector2(SimpleInput.GetAxis(HorizontalAxisName), SimpleInput.GetAxis(VerticalAxisName));
    
    protected static Vector2 UnityInputAxis() => 
      new Vector2(UnityEngine.Input.GetAxis(HorizontalAxisName), UnityEngine.Input.GetAxis(VerticalAxisName));
  }
}