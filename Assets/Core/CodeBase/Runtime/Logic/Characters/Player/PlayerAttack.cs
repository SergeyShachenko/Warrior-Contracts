using System;
using UnityEngine;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Logic.Characters
{
  public class PlayerAttack : IAttack
  {
    public event Action Attack;

    public bool IsActive { get; set; } = true;
    public float Damage => _progressData.Stats.Damage;
    public float AttackDistance => _progressData.Stats.AttackDistance;
    public float Cooldown => _progressData.Stats.Cooldown;
    public float HitRadius => _progressData.Stats.HitRadius;

    private readonly PlayerProgressData _progressData;
    private readonly CharacterController _characterController;
    private readonly Transform _transform;
    private readonly int _layerMask;

    private IInputService _inputService;
    private Collider[] _hits = new Collider[3];

    public PlayerAttack(PlayerProgressData progressData, CharacterController characterController, Transform transform)
    {
      _progressData = progressData;
      _characterController = characterController;
      _transform = transform;
      
      _inputService = AllServices.Container.Single<IInputService>(); 
      _layerMask = 1 << LayerMask.NameToLayer("Hittable");
    }
    

    public void Tick()
    {
      if (IsActive == false) return;

      
      if (_inputService.GetAttackButtonUp()) 
        StartAttack();
    }

    public void TakeDamage()
    {
      if (IsActive == false) return;
      
      
      for (var i = 0; i < Hit(); ++i) 
        _hits[i].transform.parent.parent.GetComponent<Enemy>().Health.TakeDamage(Damage);
    }

    private void StartAttack() => 
      Attack?.Invoke();

    private int Hit() => 
      Physics.OverlapSphereNonAlloc(StartPoint() + _transform.forward, AttackDistance, _hits, _layerMask);

    private Vector3 StartPoint() => 
      new(_transform.position.x, _characterController.center.y / 2, _transform.position.z);
  }
}