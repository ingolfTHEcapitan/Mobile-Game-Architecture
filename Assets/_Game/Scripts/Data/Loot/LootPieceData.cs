using System;

namespace _Game.Scripts.Data.Loot
{
    [Serializable]
    public class LootPieceData
    {
        public Loot Loot;
        public Vector3Data Position;

        public LootPieceData(Loot loot, Vector3Data position)
        {
            Loot = loot;
            Position = position;
        }
    }
}