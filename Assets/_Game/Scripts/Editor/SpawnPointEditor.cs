using _Game.Scripts.Logic.EnemySpawner;
using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.Editor
{
    [CustomEditor(typeof(SpawnPoint))]
    public class SpawnPointEditor : UnityEditor.Editor
    {
        private const float SphereRadius = 0.5f;

        [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
        public static void RenderCustomGizmo(SpawnPoint spawnPoint, GizmoType gizmo)
        {
            Gizmos.color = new Color32(90, 10, 10, 220);
            Gizmos.DrawSphere(spawnPoint.transform.position, SphereRadius);
        }
    }
}