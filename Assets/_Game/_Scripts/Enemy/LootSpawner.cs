using _Game._Scripts.Data.Enemy;
using _Game._Scripts.Infrastructure.Services.Factory;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class LootSpawner: MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private IGameFactory _factory;
        private int _lootMin;
        private int _lootMax;

        private void Start()
        {
            _enemyDeath.Died += SpawnLoot;
        }

        public void Initialize(IGameFactory factory)
        {
            _factory = factory;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }

        private void SpawnLoot()
        {
            LootPiece lootPiece =  _factory.CreateLoot();
            lootPiece.transform.position = transform.position;

            Loot loot = GenerateLoot();
            
            lootPiece.SetLoot(loot);
        }

        private Loot GenerateLoot()
        {
            Loot loot = new Loot()
            {
                Value = Random.Range(_lootMin, _lootMax)
            };
            return loot;
        }
    }
}