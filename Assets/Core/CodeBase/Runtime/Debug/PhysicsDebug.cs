using UnityEngine;

namespace WC.Runtime.DebugTools
{
  public static class PhysicsDebug
  {
    public static void DrawSphere(Vector3 position, float radius, float duration)
    {
      Debug.DrawRay(position, radius * Vector3.up, Color.red, duration);
      Debug.DrawRay(position, radius * Vector3.down, Color.red, duration);
      Debug.DrawRay(position, radius * Vector3.left, Color.red, duration);
      Debug.DrawRay(position, radius * Vector3.right, Color.red, duration);
      Debug.DrawRay(position, radius * Vector3.forward, Color.red, duration);
      Debug.DrawRay(position, radius * Vector3.back, Color.red, duration);
    }
  }
}