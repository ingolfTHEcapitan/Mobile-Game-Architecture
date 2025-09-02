using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts.StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/Level")]
    public class LevelStaticData: ScriptableObject
    {
        public string SceneKey;
        
        public List<EnemySpawnerStaticData> EnemySpawners;
    }
}