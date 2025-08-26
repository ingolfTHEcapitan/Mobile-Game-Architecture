using System;
using _Game._Scripts.Data.Player;
using UnityEngine.Rendering;

namespace _Game._Scripts.Data.Enemy
{
    [Serializable]
    public class Loot
    {
        public int Value;
        public PositionOnLevel PositionOnLevel;
    }
}