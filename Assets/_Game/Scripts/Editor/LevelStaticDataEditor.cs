using System.Collections.Generic;
using _Game.Scripts.Logic.Common;
using _Game.Scripts.Logic.EnemySpawner;
using _Game.Scripts.Services.AssetManagement;
using _Game.Scripts.StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Editor
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