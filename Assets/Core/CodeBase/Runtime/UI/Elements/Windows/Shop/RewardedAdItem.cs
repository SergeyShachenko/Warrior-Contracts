using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.UI.Elements
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
    

    public void SubscribeUpdates()
    {
      _showAdButton.onClick.AddListener(OnShowAdClick);
      _adsService.RewardedReady += Refresh;
    }

    public void UnsubscribeUpdates()
    {
      _showAdButton.onClick.RemoveListener(OnShowAdClick);
      _adsService.RewardedReady -= Refresh;
    }

    public void Refresh()
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