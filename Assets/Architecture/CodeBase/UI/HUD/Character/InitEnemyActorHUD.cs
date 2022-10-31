using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.HUD.Character
{
  public class InitEnemyActorHUD : MonoBehaviour
  {
    [SerializeField] private ActorHUD _hud;


    private void Awake() => 
      _hud.Construct(transform.parent.GetComponent<IHealth>());
  }
}