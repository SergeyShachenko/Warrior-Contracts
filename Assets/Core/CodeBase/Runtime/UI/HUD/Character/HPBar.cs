using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD.Character
{
  public class HPBar : MonoBehaviour
  {
    [SerializeField] private Image _progressBar;


    public void SetProgress(float currentValue, float maxValue) =>
      _progressBar.fillAmount = currentValue / maxValue;
  }
}