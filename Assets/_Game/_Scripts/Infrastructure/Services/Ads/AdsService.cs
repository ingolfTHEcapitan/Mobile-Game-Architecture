using System;
using UnityEngine;
using UnityEngine.Advertisements;
using Application = UnityEngine.Device.Application;

namespace _Game._Scripts.Infrastructure.Services.Ads
{
    public class AdsService: IAdsService, IUnityAdsListener
    {
        private const string AndroidGameId = "5939121";
        private const string IOSGameId = "5939120";
        private const string AndroidRewardedVideoId = "Rewarded_Android";
        
        public event Action RewardedVideoReady;
        private event Action _onVideoFinished;

        public bool IsRewardedVideoReady =>
            Advertisement.IsReady(AndroidRewardedVideoId);
        
        public int Reward => 30;

        public void Initialize()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(GetGameId());
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            Advertisement.Show(AndroidRewardedVideoId);
            
            _onVideoFinished = onVideoFinished;
        }

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log($"OnUnityAdsReady: {placementId}");

            if (placementId == AndroidRewardedVideoId) 
                RewardedVideoReady?.Invoke();
        }

        public void OnUnityAdsDidError(string message) => 
            Debug.Log($"OnUnityAdsDidError: {message}");

        public void OnUnityAdsDidStart(string placementId) => 
            Debug.Log($"OnUnityAdsDidStart: {placementId}");

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    Debug.LogError($"OnUnityAdsDidFinish: {showResult}");
                    break;
                case ShowResult.Skipped:
                    Debug.LogError($"OnUnityAdsDidFinish: {showResult}");
                    break;
                case ShowResult.Finished:
                    _onVideoFinished?.Invoke();
                    break;
                default:
                    Debug.LogError($"OnUnityAdsDidFinish: {showResult}");
                    break;
            }
            
            // Обнуляем это событие, для того что бы ели кто-то следующий захочет показать рекламу
            // ему бы пришлось заново задавать событие.
            // Это сделано для того что бы мы не вызывали событие на тех штуках которые могут уже не существовать
            _onVideoFinished = null;
        }
        
        private string GetGameId()
        {
            string gameId = string.Empty;

            if (Application.platform == RuntimePlatform.Android)
                gameId = AndroidGameId;
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                gameId = IOSGameId;
            else if (Application.platform == RuntimePlatform.WindowsEditor)
                gameId = AndroidGameId;
            else
                Debug.LogError("Unsupported platform for ads ");

            return gameId;
        }
    }
}