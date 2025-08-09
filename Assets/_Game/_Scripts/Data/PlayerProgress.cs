using System;
using UnityEngine.Serialization;

namespace _Game._Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        public HeroStats heroStats;
        public KillData KillData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new HeroState();
            heroStats = new HeroStats();
            KillData = new KillData();
        }
    }
}