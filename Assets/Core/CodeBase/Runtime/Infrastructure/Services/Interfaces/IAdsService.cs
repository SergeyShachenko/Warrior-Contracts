using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IAdsService : IService
  {
    event Action RewardedReady;
    bool IsRewardedReady { get; }
    int Reward { get; }
    void Init();
    void ShowRewardedVideo(Action onVideoFinished);
  }
}