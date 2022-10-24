using System;
using CodeBase.Logic.Characters.Hero;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD
{
  public class HUDButton : MonoBehaviour
  {
    [SerializeField] private Button _button;
    
    private HeroAnimator _heroAnimator;


    private void Awake()
    {
      _heroAnimator = FindObjectOfType<HeroAnimator>();
      _button.onClick.AddListener(OnClick);
    }

    
    private void OnClick()
    {
      _heroAnimator.PlayAttack();
    }
  }
}