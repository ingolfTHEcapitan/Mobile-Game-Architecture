using _Game._Scripts.Infrastructure.Services.Ads;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.UI.Windows.Shop
{
    public class RewardedAdItem: MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private GameObject[] _adAvailableObjects;
        [SerializeField] private GameObject[] _adUnavailableObjects;
        
        private IAdsService _adsService;
        private IPersistantProgressService _progressService;
        
        public void Inject(IAdsService adsService, IPersistantProgressService progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }
        
        public void Initialize()
        {
            _showAdButton.onClick.AddListener(OnShowAdClicked);

            UpdateQuantityText();
            RefreshAvailableAd();
        }

        public void SubscribeUpdates() => 
            _adsService.RewardedVideoReady += RefreshAvailableAd;

        public void UnSubscribe() => 
            _adsService.RewardedVideoReady -= RefreshAvailableAd;

        private void OnShowAdClicked() => 
            _adsService.ShowRewardedVideo(OnVideoFinished);

        private void OnVideoFinished() => 
            _progressService.Progress.WorldData.LootData.Add(_adsService.Reward);

        private void RefreshAvailableAd()
        {
            bool videoReady = _adsService.IsRewardedVideoReady;

            foreach (GameObject adAvailableObject in _adAvailableObjects) 
                adAvailableObject.SetActive(videoReady);
            
            foreach (GameObject adUnavailableObject in _adUnavailableObjects) 
                adUnavailableObject.SetActive(!videoReady);
        }

        private void UpdateQuantityText() => 
            _quantityText.text = _adsService.Reward.ToString();
    }
}