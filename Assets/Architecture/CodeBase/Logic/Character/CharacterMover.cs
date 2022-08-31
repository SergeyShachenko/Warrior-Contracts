using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Logic.Character
{
  public class CharacterMover : MonoBehaviour
  {
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed = 8f;

    private IInputService _inputService;


    private void Awake()
    {
      _inputService = AllServices.Container.Single<IInputService>();
    }

    private void Update()
    {
      Vector3 movementDirection = Vector3.zero;

      if (_inputService.AxisDirection.sqrMagnitude > Constants.Epsilon)
      {
        movementDirection = UnityEngine.Camera.main.transform.TransformDirection(_inputService.AxisDirection);
        movementDirection.y = 0f;
        movementDirection.Normalize();

        transform.forward = movementDirection;
      }

      movementDirection += Physics.gravity;
      
      
      _characterController.Move(movementDirection * _movementSpeed * Time.deltaTime);
    }
  }
}