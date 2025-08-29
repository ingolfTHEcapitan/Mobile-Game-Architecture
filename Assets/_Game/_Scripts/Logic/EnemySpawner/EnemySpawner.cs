using _Game._Scripts.Data.Player;
using _Game._Scripts.Enemy;
using _Game._Scripts.Infrastructure.Services.Factory;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using _Game._Scripts.StaticData;
using UnityEngine;

namespace _Game._Scripts.Logic.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public EnemyTypeId EnemyTypeId;
        public string Id;

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        private bool _slain;


        public void Initialize(IGameFactory factory)
        {
            _factory = factory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.SlainSpawners.Contains(Id))
                _slain = true;
            else
                Spawn();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.SlainSpawners.Add(Id);
        }

        private void Spawn()
        {
            GameObject enemy = _factory.CreateEnemy(EnemyTypeId, transform);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.Died += Slay;
        }

        private void Slay()
        {
            _enemyDeath.Died -= Slay;
            _slain = true;
        }
    }
}