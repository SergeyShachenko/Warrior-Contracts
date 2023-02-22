using UnityEngine;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.UI.HUD
{
  public class InitEnemyActorHUD : MonoBehaviour
  {
    [SerializeField] private ActorHUD _hud;


    private void Awake() => 
      _hud.Construct(transform.parent.GetComponent<IHealth>());
  }
}