using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace WC.Runtime.Infrastructure.Services
{
  public class AdsService : IAdsService, IUnityAdsListener
  {
    public event Action RewardedReady;

    public int Reward => 13;
    public bool IsRewardedReady => Advertisement.IsReady(RewAndroidID);

    private const string AndroidGameID = "5121551";
    private const string iOSGameID = "5121550";
    private const string RewAndroidID = "Rewarded_Android";
    private const string RewIOSID = "Rewarded_iOS";

    private event Action OnVideoFinished;
    
    private string _currentGameID;

    public AdsService()
    {
      switch (Application.platform)
      {
        case RuntimePlatform.WindowsEditor: _currentGameID = AndroidGameID; break;
        case RuntimePlatform.IPhonePlayer: _currentGameID = iOSGameID; break;
        case RuntimePlatform.Android: _currentGameID = AndroidGameID; break;
        default: Debug.LogWarning("Для текущей платформы не настроена поддержка встроенной рекламы"); break;
      }
      
      Advertisement.AddListener(this);
      Advertisement.Initialize(_currentGameID);
    }


    public void ShowRewardedVideo(Action onVideoFinished)
    {
      Advertisement.Show(RewAndroidID);
      OnVideoFinished = onVideoFinished;
    }

    public void OnUnityAdsReady(string placementId)
    {
      Debug.Log($"UnityAds: Placement <b>{placementId}</b> <color=Green>is ready</color>");

      if (placementId == RewAndroidID) 
        RewardedReady?.Invoke();
    }

    public void OnUnityAdsDidError(string message) => 
      Debug.LogError($"UnityAds: {message}");

    public void OnUnityAdsDidStart(string placementId) => 
      Debug.Log($"UnityAds: Placement <b>{placementId}</b> <color=Green>starts</color>");

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
      switch (showResult)
      {
        case ShowResult.Failed: Debug.LogError($"UnityAds: Placement <b>{placementId}</b> <color=Red>{showResult}</color>"); break;
        case ShowResult.Skipped: Debug.LogError($"UnityAds: Placement <b>{placementId}</b> <color=Red>{showResult}</color>"); break;
        case ShowResult.Finished:
        {
          OnVideoFinished?.Invoke();
          Debug.Log($"UnityAds: Placement <b>{placementId}</b> <color=Green>{showResult}</color>");
        }
          break;
        
        default: Debug.Log($"UnityAds: Placement <b>{placementId}</b> is <color=Blue>{showResult}</color>"); break;
      }

      OnVideoFinished = null;
    }
  }
}