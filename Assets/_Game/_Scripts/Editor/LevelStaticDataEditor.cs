using System.Collections.Generic;
using System.Linq;
using _Game._Scripts.Infrastructure.Services.StaticData;
using _Game._Scripts.Logic;
using _Game._Scripts.Logic.EnemySpawner;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game._Scripts.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = (LevelStaticData)target;

            if (GUILayout.Button("Collect All Spawn Points"))
            {
                List<EnemySpawnerStaticData> list = new List<EnemySpawnerStaticData>();
                
                foreach (var spawnPoint in FindObjectsOfType<SpawnPoint>())
                {
                    var spawnerId = spawnPoint.GetComponent<UniqueId>().Id;
                    var spawnerData = new EnemySpawnerStaticData(spawnerId, spawnPoint.EnemyTypeId, spawnPoint.transform.position);
                    
                    list.Add(spawnerData);
                }

                levelData.EnemySpawners = list;

                levelData.SceneKey = SceneManager.GetActiveScene().name;
            }

            EditorUtility.SetDirty(levelData);
        }
    }
}