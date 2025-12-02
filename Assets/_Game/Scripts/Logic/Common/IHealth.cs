using System;

namespace _Game.Scripts.Logic.Common
{
    public interface IHealth
    {
        float Current { get; set; }
        float Max { get; set; }
        event Action HealthChanged;
        void TakeDamage(float damage);
    }
}