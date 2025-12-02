using System;
using _Game.Scripts.Data.Loot;

namespace _Game.Scripts.Data.Player
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public LootData LootData;
        
        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            LootData = new LootData();
        }
    }
}