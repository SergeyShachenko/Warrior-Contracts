using UnityEngine;

namespace Architecture.Scripts.Logic.Camera
{
  public class CameraMover : MonoBehaviour
  {
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _distance = 6f;
    [SerializeField] private float _rotationAngleX = 35f;
    [SerializeField] private float FollowingOffsetY = 2f;


    private void LateUpdate()
    {
      if (_followTarget == null) return;


      Quaternion rotation = Quaternion.Euler(_rotationAngleX, _followTarget.transform.localRotation.y, 0f);
      Vector3 position = rotation * new Vector3(0f, 0f, -_distance) + GetFollowingPos();

      transform.localPosition = position;
      transform.localRotation = rotation;
    }

    public void SetFollowTarget(GameObject target) => 
      _followTarget = target.transform;

    private Vector3 GetFollowingPos()
    {
      Vector3 followingPos = _followTarget.localPosition;
      followingPos.y += FollowingOffsetY;
      
      return followingPos;
    }
  }
}