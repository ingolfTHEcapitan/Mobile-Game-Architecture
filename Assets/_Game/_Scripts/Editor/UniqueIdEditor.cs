using System.Linq;
using _Game._Scripts.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace _Game._Scripts.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueId uniqueId = (UniqueId)target;
            
            if(IsPrefab(uniqueId))
                return;
            
            if (string.IsNullOrEmpty(uniqueId.Id))
                Generate(uniqueId);
            else
                ReGenerate(uniqueId);
        }

        private bool IsPrefab(UniqueId uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.GenerateId();

            if (!Application.isPlaying)
            {
                // Говорим Юнити что мы изменили один из её компонентов к коде
                // Теперь она должна его пересохранить, так же пересохраняем сцену.
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }

        private void ReGenerate(UniqueId uniqueId)
        {
            UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

            if(uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                Generate(uniqueId);
        }
    
        private string GetSceneName(UniqueId uniqueId)
        {
            return uniqueId.gameObject.scene.name;
        }
    }
}