using System;

namespace _Game._Scripts.Infrastructure.Services.Ads
{
    public interface IAdsService: IService
    {
        event Action RewardedVideoReady;
        int Reward { get;}
        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
        
        bool IsRewardedVideoReady { get;}
    }
}