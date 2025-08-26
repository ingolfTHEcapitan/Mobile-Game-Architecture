using System;
using _Game._Scripts.Data.Enemy;

namespace _Game._Scripts.Data.Player
{
    [Serializable]
    public class LootDictionaryPiece
    {
        public Loot Loot;
        public string LootId;

        public LootDictionaryPiece(Loot loot, string lootId)
        {
            Loot = loot;
            LootId = lootId;
        }
    }
}