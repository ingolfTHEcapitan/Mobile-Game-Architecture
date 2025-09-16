using System;
using System.Collections.Generic;

namespace _Game._Scripts.Data.IAP
{
    [Serializable]
    public class PurchaseData
    {
        public List<BoughtIAP> BoughtIAPs = new List<BoughtIAP>();

        public event Action Changed;
        
        
        public void AddPurchase(string id)
        {
            BoughtIAP boughtIap = GetProduct(id);

            if (boughtIap != null) 
                boughtIap.Count++;
            else
                BoughtIAPs.Add(new BoughtIAP(id, count: 1));
            
            Changed?.Invoke();
        }

        private BoughtIAP GetProduct(string id)
        {
            return BoughtIAPs.Find(x=> x.IAPId == id);
        }
    }
}