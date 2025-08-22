using System;

namespace _Game._Scripts.Data.Player
{
    [Serializable]
    public class HeroState
    {
        public float CurrentHealth;
        public float MaxHealth;

        public void ResetHealth() => CurrentHealth = MaxHealth;
    }
}