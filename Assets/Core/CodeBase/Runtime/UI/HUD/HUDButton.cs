using CodeBase.Logic.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD
{
  public class HUDButton : MonoBehaviour
  {
    [SerializeField] private Button _button;
    
    private PlayerAnimator _playerAnimator;


    private void Awake()
    {
      _playerAnimator = FindObjectOfType<PlayerAnimator>();
      _button.onClick.AddListener(OnClick);
    }

    
    private void OnClick()
    {
      _playerAnimator.PlayAttack();
    }
  }
}