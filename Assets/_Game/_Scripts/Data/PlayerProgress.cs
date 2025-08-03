using System;
using UnityEngine.Serialization;

namespace _Game._Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        [FormerlySerializedAs("Stats")] public HeroStats heroStats;
        
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new HeroState();
            heroStats = new HeroStats();
        }
    }
}