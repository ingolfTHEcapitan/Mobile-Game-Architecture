using _Game._Scripts.Logic.Triggers;
using UnityEditor;
using UnityEngine;

namespace _Game._Scripts.Editor
{
    [CustomEditor(typeof(TriggerBase))]
    public class TriggerBaseEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
        public static void RenderCustomGizmo(TriggerBase trigger, GizmoType gizmo)
        {
            if (trigger.BoxCollider is null)
                return;

            Gizmos.color = trigger.BoxColor;
            Gizmos.DrawCube(trigger.transform.position
                            + trigger.BoxCollider.center, trigger.BoxCollider.size);
        }
    }
}