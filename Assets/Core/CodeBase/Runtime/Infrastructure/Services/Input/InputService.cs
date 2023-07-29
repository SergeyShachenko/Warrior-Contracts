using UnityEngine;

namespace WC.Runtime.Infrastructure.Services
{
  public abstract class InputService : IInputService
  {
    public abstract Vector2 AxisDirection { get; }
    public abstract Vector2 LookDelta { get; }

    private const string VerticalAxisName = "Vertical";
    private const string HorizontalAxisName = "Horizontal";
    private const string AttackButtonName = "Attack";

    
    public bool SimpleInputGetAttackButtonUp() => 
      SimpleInput.GetButtonUp(AttackButtonName);
    
    public bool UnityGetAttackButton() => 
      Input.GetKeyDown(KeyCode.Mouse0);

    public bool UnityGetSlowWalkButton() => 
      Input.GetKey(KeyCode.LeftControl);

    public bool UnityGetRunButton() => 
      Input.GetKey(KeyCode.LeftShift);
      
    public bool UnityGetAimButton() => 
      Input.GetKey(KeyCode.Mouse1);

    
    protected Vector2 SimpleInputAxis() => new 
      (SimpleInput.GetAxis(HorizontalAxisName), SimpleInput.GetAxis(VerticalAxisName));

    protected Vector2 UnityInputAxis() => new
      (Input.GetAxisRaw(HorizontalAxisName), Input.GetAxisRaw(VerticalAxisName));

    protected Vector2 UnityLookDelta() => new
      (Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
  }
}