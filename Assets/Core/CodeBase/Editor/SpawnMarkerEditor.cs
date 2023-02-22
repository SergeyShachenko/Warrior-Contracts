using UnityEditor;
using UnityEngine;
using WC.Runtime.Logic.Characters;

namespace WC.Editor
{
  [CustomEditor(typeof(SpawnMarker))]
  public class SpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(SpawnMarker spawnPoint, GizmoType gizmo)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(spawnPoint.transform.position, 0.5f);
    }
  }
}