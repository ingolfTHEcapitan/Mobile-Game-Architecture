using System;

namespace _Game._Scripts.Data.Loot
{
    [Serializable]
    public class LootData
    {
        public int Collected;
        public LootPieceDataDictionary LootPiecesOnScene = new LootPieceDataDictionary();

        public event Action Changed;
        
        public void Collect(Loot loot)
        {
            Collected += loot.Value;
            Changed?.Invoke();
        }
    }
}