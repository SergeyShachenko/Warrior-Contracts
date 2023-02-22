using UnityEngine;
using UnityEngine.UI;

namespace WC.Runtime.UI.HUD
{
  public class HPBar : MonoBehaviour
  {
    [SerializeField] private Image _progressBar;


    public void SetProgress(float currentValue, float maxValue) =>
      _progressBar.fillAmount = currentValue / maxValue;
  }
}