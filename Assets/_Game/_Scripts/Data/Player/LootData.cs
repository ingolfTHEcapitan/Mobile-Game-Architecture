using System;
using _Game._Scripts.Data.Enemy;

namespace _Game._Scripts.Data.Player
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public event Action Changed;

        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }
    }
}