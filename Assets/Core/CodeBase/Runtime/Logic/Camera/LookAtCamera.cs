using UnityEngine;

namespace WC.Runtime.Logic.Camera
{
  public class LookAtCamera : MonoBehaviour
  {
    private void Update()
    {
      if (UnityEngine.Camera.main == null) return;
      
      
      Quaternion rotation = UnityEngine.Camera.main.transform.rotation;
      transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
  }
}