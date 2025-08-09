using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(UniqueId))]
public class UniqueIdEditor : Editor
{
    private void OnEnable()
    {
        var uniqueId = (UniqueId)target;

        if (string.IsNullOrEmpty(uniqueId.Id))
            Generate(uniqueId);
        else
            ReGenerate(uniqueId);
    }

    private void Generate(UniqueId uniqueId)
    {
        uniqueId.Id = GetSceneName(uniqueId) + Guid.NewGuid().ToString();

        if (Application.isPlaying)
            return;

        // Говорим Юнити что мы изменили один из её компонентов к коде
        // Теперь она должна его пересохранить, так же пересохраняем сцену.
        EditorUtility.SetDirty(uniqueId);
        EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);

    }

    private void ReGenerate(UniqueId uniqueId)
    {
        UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

        foreach (var otherUniqueId in uniqueIds)
        {
            if (otherUniqueId != uniqueId && otherUniqueId.Id == uniqueId.Id)
                Generate(uniqueId);
        }
    }
    
    private string GetSceneName(UniqueId uniqueId)
    {
        return uniqueId.gameObject.scene.name;
    }
}