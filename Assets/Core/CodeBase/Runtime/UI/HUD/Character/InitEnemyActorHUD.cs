using UnityEngine;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.UI.HUD
{
  public class InitEnemyActorHUD : MonoBehaviour
  {
    [SerializeField] private ActorHUD _hud;
    [SerializeField] private Enemy _enemy;


    private void Awake() => 
      _enemy.Initialized += OnEnemyInit;

    
    private void OnEnemyInit() => 
      _hud.Construct(_enemy.Health);
  }
}