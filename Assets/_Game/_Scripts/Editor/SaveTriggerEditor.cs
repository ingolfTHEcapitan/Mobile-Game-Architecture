using UnityEngine;
using UnityEditor;
using _Game._Scripts.Logic;

[CustomEditor(typeof(SaveTrigger))]
public class SaveTriggerEditor : Editor
{
     [DrawGizmo(GizmoType.Active | GizmoType.NonSelected | GizmoType.Pickable)]
    public static void RenderCustomGismo(SaveTrigger saveTrigger, GizmoType gismo)
    {
       if (saveTrigger.BoxCollider is null)
                return; 
            
        Gizmos.color = new Color32(15, 161, 49, 130);
        Gizmos.DrawCube(saveTrigger.transform.position
            + saveTrigger.BoxCollider.center, saveTrigger.BoxCollider.size);
    }
}