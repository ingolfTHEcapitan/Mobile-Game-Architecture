using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    private static float _sphereRadius = 0.5f;

    [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
    public static void RenderCustomGismo(EnemySpawner spawner, GizmoType gismo)
    {
        Gizmos.color = new Color32(90, 10, 10, 220);
        Gizmos.DrawSphere(spawner.transform.position, _sphereRadius);
    }
}