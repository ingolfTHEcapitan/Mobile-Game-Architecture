using System.Threading.Tasks;
using _Game.Scripts.Data.Player;
using _Game.Scripts.Logic.Enemy;
using _Game.Scripts.Services.Factory;
using _Game.Scripts.Services.PersistantProgress;
using _Game.Scripts.StaticData;
using UnityEngine;

namespace _Game.Scripts.Logic.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public EnemyTypeId EnemyTypeId;
        public string Id;

        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;
        private bool _slain;
        
        public void Construct(IGameFactory factory) => 
            _factory = factory;

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

        private async Task Spawn()
        {
            GameObject enemy = await _factory.CreateEnemy(EnemyTypeId, transform);
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