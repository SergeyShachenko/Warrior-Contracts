using UnityEngine;

namespace WC.Runtime.Logic.Characters
{
  public class RotateToPlayerAI : FollowAIBase
  {
    public float Speed
    {
      get => _speed;
      set => _speed = value;
    }

    [SerializeField] private float _speed;

    private Vector3 _lookAt;
    

    private void Update()
    {
      if (p_EnemyDeath.IsDead == false)
        RotateToTarget();
    }

    
    private void RotateToTarget()
    {
      UpdateLookAt();

      transform.rotation = SmoothRotation(currentRotation: transform.rotation, targetRotation: _lookAt);
    }

    private void UpdateLookAt()
    {
      Vector3 currentPos = transform.position;
      Vector3 targetPos = p_Player.transform.position;
      Vector3 lookDirection = targetPos - currentPos;
      
      _lookAt = new Vector3(lookDirection.x, currentPos.y, lookDirection.z);
    }

    private Quaternion SmoothRotation(Quaternion currentRotation, Vector3 targetRotation) =>
      Quaternion.Lerp(currentRotation, Quaternion.LookRotation(targetRotation), _speed * Time.deltaTime);
  }
}