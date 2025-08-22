using System;

namespace _Game._Scripts.Data.Player
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}