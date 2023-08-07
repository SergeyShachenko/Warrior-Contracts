using UnityEngine;

namespace WC.Runtime.Gameplay.Logic
{
  public class LookAtCamera : MonoBehaviour
  {
    private void Update()
    {
      if (Camera.main == null) return;
      
      
      Quaternion rotation = Camera.main.transform.rotation;
      transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
    }
  }
}