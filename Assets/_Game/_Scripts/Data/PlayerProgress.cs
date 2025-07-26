using System;

namespace _Game._Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public HeroState HeroState;
        
        public PlayerProgress(string initialLevel)
        {
            WorldData = new WorldData(initialLevel);
            HeroState = new HeroState();
        }
    }

    [Serializable]
    public class HeroState
    {
        public float CurrentHealth;
        public float MaxHealth;

        public void ResetHealth() => CurrentHealth = MaxHealth;
    }
}