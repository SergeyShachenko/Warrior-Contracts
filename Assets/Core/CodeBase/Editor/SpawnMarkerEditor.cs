using UnityEditor;
using UnityEngine;
using WC.Runtime.Logic.Characters;

namespace WC.Editor
{
  [CustomEditor(typeof(WarriorSpawnMarker))]
  public class SpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(WarriorSpawnMarker warriorSpawnPoint, GizmoType gizmo)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(warriorSpawnPoint.transform.position, 0.5f);
    }
  }
}