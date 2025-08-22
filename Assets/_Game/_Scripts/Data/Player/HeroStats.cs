using System;
using UnityEngine.Serialization;

namespace _Game._Scripts.Data.Player
{
    [Serializable]
    public class HeroStats
    {
        [FormerlySerializedAs("AttackDamage")] public float AttackDamage;
        public float AttackDistance;
        public float AttackRadius;
    }
}