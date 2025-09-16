using System;

namespace _Game._Scripts.Data.IAP
{
    [Serializable]
    public class BoughtIAP
    {
        public string IAPId;
        public int Count;

        public BoughtIAP(string iapId, int count)
        {
            IAPId = iapId;
            Count = count;
        }
    }
}