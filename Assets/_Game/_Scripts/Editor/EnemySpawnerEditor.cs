using _Game._Scripts.Logic;
using UnityEditor;
using UnityEngine;

namespace _Game._Scripts.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        private static float _sphereRadius = 0.5f;

        [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
        public static void RenderCustomGismo(EnemySpawner spawner, GizmoType gismo)
        {
            Gizmos.color = new Color32(90, 10, 10, 220);
            Gizmos.DrawSphere(spawner.transform.position, _sphereRadius);
        }
    }
}