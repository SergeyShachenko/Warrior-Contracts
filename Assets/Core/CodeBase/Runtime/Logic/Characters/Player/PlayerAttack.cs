using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerAttack : CharacterAttackBase
  {
    private readonly IInputService _inputService;

    private readonly CharacterController _characterController;
    private readonly Transform _parent;
    private readonly Collider[] _hits = new Collider[3];
    
    private readonly int _layerMask;

    public PlayerAttack(
      CombatStatsData data,
      IInputService inputService,
      CharacterController characterController, 
      Transform parent)
    : base(data)
    {
      _inputService = inputService;
      _characterController = characterController;
      _parent = parent;
      _layerMask = 1 << LayerMask.NameToLayer("Hittable");
    }


    public override void Tick()
    {
      if (IsActive == false) return;

      
      if (_inputService.GetAttackButtonUp()) 
        StartAttack();
    }

    
    public override void TakeDamage()
    {
      if (IsActive == false) return;
      
      
      for (var i = 0; i < Hit(); ++i) 
        _hits[i].transform.parent.parent.GetComponent<Enemy>().Health.TakeDamage(Damage);
    }

    public override void StopAttack() { }


    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + _parent.forward, AttackDistance, _hits, _layerMask);

    private Vector3 StartPoint() => 
      new(_parent.position.x, _characterController.center.y / 2, _parent.position.z);
  }
}