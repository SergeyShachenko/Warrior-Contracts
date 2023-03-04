using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI
{
  public class RewardedAdItem : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] private Button _showAdButton;
    [SerializeField] private List<GameObject> _adActiveObj, _adInactiveObj;
    
    private IAdsService _adsService;
    private IPersistentProgressService _progress;

    public void Construct(IAdsService adsService, IPersistentProgressService  progress)
    {
      _adsService = adsService;
      _progress = progress;
    }
    
    public void Init()
    {
      _showAdButton.onClick.AddListener(OnShowAdClick);

      RefreshActiveAd();
    }


    public void Subscribe() => 
      _adsService.RewardedReady += RefreshActiveAd;

    public void CleanUp() => 
      _adsService.RewardedReady -= RefreshActiveAd;

    private void RefreshActiveAd()
    {
      var isRewardedReady = _adsService.IsRewardedReady;

      foreach (GameObject obj in _adActiveObj) 
        obj.SetActive(isRewardedReady);
      
      foreach (GameObject obj in _adInactiveObj) 
        obj.SetActive(isRewardedReady == false);
    }

    private void OnShowAdClick() => 
      _adsService.ShowRewardedVideo(OnVideoFinished);

    private void OnVideoFinished() => 
      _progress.Player.World.Loot.Add(_adsService.Reward);
  }
}