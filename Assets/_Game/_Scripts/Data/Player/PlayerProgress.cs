using System;
using _Game._Scripts.Data.IAP;

namespace _Game._Scripts.Data.Player
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        public HeroStats HeroStats;
        public KillData KillData;
        public PurchaseData PurchaseData;

        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new HeroState();
            HeroStats = new HeroStats();
            KillData = new KillData();
            PurchaseData = new PurchaseData();
        }
    }
}