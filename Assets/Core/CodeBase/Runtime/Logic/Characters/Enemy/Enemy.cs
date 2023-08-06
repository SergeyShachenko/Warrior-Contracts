using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using WC.Runtime.Data.Characters;

namespace WC.Runtime.Logic.Characters
{
  public class Enemy : CharacterBase
  {
    public EnemyWarriorID ID { get; private set; }
    
    [SerializeField] private NavMeshAgent _agent;

    private EnemyStatsData _data;
    private Player _player;

    public void SetData(EnemyWarriorID id, EnemyStatsData data, Player player)
    {
      ID = id;
      _data = data;
      _player = player;
    }

    
    protected override void Init()
    {
      Health = new EnemyHealth(_data.Life);
      Death = new EnemyDeath();
      Attack = new EnemyAttack(this, _data.Combat, _player);
      Movement = new EnemyMovement(this, _data.Movement, _agent);
      Animator = new EnemyAnimator(this, p_Animator);
    }

    protected override void OnDeath()
    {
      base.OnDeath();
      StartCoroutine(DestroyBody());
    }

    
    private IEnumerator DestroyBody()
    {
      yield return new WaitForSeconds(3);

      Destroy(gameObject);
    }
  }
}