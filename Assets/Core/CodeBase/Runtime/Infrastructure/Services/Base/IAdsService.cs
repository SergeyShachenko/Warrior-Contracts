using System;

namespace WC.Runtime.Infrastructure.Services
{
  public interface IAdsService
  {
    event Action RewardedReady;
    bool IsRewardedReady { get; }
    int Reward { get; }
    void ShowRewardedVideo(Action onVideoFinished);
  }
}