using UnityEngine;
using UnityEngine.UI;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.UI
{
  public class HUDButton : MonoBehaviour
  {
    [SerializeField] private Button _button;

    private Player _player;
    private PlayerAnimator _playerAnimator;


    private void Awake()
    {
      _player = FindObjectOfType<Player>();
      _player.Initialized += OnPlayerInit;
      
      _button.onClick.AddListener(OnClick);
    }

    
    private void OnClick()
    {
      _playerAnimator.PlayAttack();
    }

    private void OnPlayerInit()
    {
      _playerAnimator = (PlayerAnimator)_player.Animator;
    }
  }
}