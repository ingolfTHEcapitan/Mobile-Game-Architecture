using System.Collections.Generic;
using _Game._Scripts.Infrastructure.Services.AssetManagement;
using _Game._Scripts.Logic;
using _Game._Scripts.Logic.EnemySpawner;
using _Game._Scripts.StaticData;
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

            if (GUILayout.Button("Collect All"))
            {
                levelData.EnemySpawners = CollectEnemySpawnersData();;
                levelData.SceneKey = SceneManager.GetActiveScene().name;
                levelData.PlayerInitialPoint = GameObject.FindWithTag(Tags.PlayerInitialPoint).transform.position;
            }

            EditorUtility.SetDirty(levelData);
        }

        private static List<EnemySpawnerStaticData> CollectEnemySpawnersData()
        {
            List<EnemySpawnerStaticData> list = new List<EnemySpawnerStaticData>();

            foreach (var spawnPoint in FindObjectsOfType<SpawnPoint>())
            {
                var spawnerId = spawnPoint.GetComponent<UniqueId>().Id;
                var spawnerData = new EnemySpawnerStaticData(spawnerId, spawnPoint.EnemyTypeId, spawnPoint.transform.position);
                    
                list.Add(spawnerData);
            }

            return list;
        }
    }
}