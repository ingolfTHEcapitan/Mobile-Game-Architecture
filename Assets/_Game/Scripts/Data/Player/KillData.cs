using System;
using System.Collections.Generic;

namespace _Game.Scripts.Data.Player
{
    [Serializable]
    public class KillData
    {
        public List<string> SlainSpawners;

        public KillData()
        {
            SlainSpawners = new List<string>();
        }
    }
}