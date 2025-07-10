using UnityEngine;
using UnityEngine.Serialization;

namespace _Game._Scripts.Infrastructure
{
    public class GameRunner: MonoBehaviour
    {
        [FormerlySerializedAs("_bootstrapper")] [SerializeField] private GameBootstrapper _bootstrapperPrefab;

        private void Awake()
        {
            var _bootstrapper = FindObjectOfType<GameBootstrapper>();
            
            if (_bootstrapper is null)
                Instantiate(_bootstrapperPrefab);
            
        }
    }
}