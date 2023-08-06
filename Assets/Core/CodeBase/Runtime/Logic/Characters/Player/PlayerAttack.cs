using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerAttack : CharacterAttackBase
  {
    private readonly Player _player;
    private readonly Transform _transform;
    private readonly IInputService _inputService;
    private readonly Collider[] _hits = new Collider[3];

    private readonly int _layerMask;

    public PlayerAttack(
      Player player,
      CombatStatsData data,
      IInputService inputService)
    : base(player, data)
    {
      _player = player;
      _transform = _player.transform;
      _inputService = inputService;
      _layerMask = 1 << LayerMask.NameToLayer("Hittable");
    }


    public override void Tick()
    {
      if (IsActive == false) return;

      
      if (_inputService.SimpleInputGetAttackButtonUp() || _inputService.UnityGetAttackButton()) 
        Start();

      IsAiming = _inputService.UnityGetAimButton();
    }

    
    public override void DoDamage()
    {
      if (IsActive == false) return;
      
      
      for (var i = 0; i < Hit(); ++i) 
        _hits[i].transform.parent.parent.GetComponent<Enemy>().Health.TakeDamage(Damage);
      
      Stop();
    }
    

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + _transform.forward, AttackDistance, _hits, _layerMask);

    private Vector3 StartPoint() => new
      (_transform.position.x, _player.Controller.center.y / 2, _transform.position.z);
  }
}