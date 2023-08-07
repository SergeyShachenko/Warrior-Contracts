using UnityEditor;
using UnityEngine;
using WC.Runtime.Gameplay.Logic;

namespace WC.Editor
{
  [CustomEditor(typeof(EnemySpawnMarker))]
  public class SpawnMarkerEditor : UnityEditor.Editor
  {
    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void RenderCustomGizmo(EnemySpawnMarker spawnMarker, GizmoType gizmo)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(spawnMarker.transform.position, 0.5f);
    }
  }
}