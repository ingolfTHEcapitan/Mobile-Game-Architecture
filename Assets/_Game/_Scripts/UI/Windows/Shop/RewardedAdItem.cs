using System;
using _Game._Scripts.Infrastructure.Services.Ads;
using _Game._Scripts.Infrastructure.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game._Scripts.UI.Windows.Shop
{
    public class RewardedAdItem: MonoBehaviour
    {
        [SerializeField] private Button _showAdButton;
        [SerializeField] private GameObject[] AdAvailableObjects;
        [SerializeField] private GameObject[] AdUnavailableObjects;
        
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

            foreach (GameObject adAvailableObject in AdAvailableObjects) 
                adAvailableObject.SetActive(videoReady);
            
            foreach (GameObject adUnavailableObject in AdUnavailableObjects) 
                adUnavailableObject.SetActive(!videoReady);
        }
    }
}