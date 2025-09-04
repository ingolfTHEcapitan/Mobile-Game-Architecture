using TMPro;
using UnityEngine;

namespace _Game._Scripts.UI.Windows
{
    public class ShopWindow: WindowBase
    {
        [SerializeField] private TextMeshProUGUI _CurrencyText;
        
        protected override void Initialize() => 
            UpdateCurrencyText();

        protected override void SubscribeUpdates() => 
            Progress.WorldData.LootData.Changed += UpdateCurrencyText;

        protected override void UnSubscribe()
        {
            base.UnSubscribe();
            Progress.WorldData.LootData.Changed -= UpdateCurrencyText;
        }
        
        private void UpdateCurrencyText() => 
            _CurrencyText.text = Progress.WorldData.LootData.Collected.ToString();
    }
}