using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using WC.Runtime.Gameplay.Data;
using WC.Runtime.Gameplay.Tools;
using WC.Runtime.Infrastructure.Services;
using Zenject;

namespace WC.Runtime.Gameplay.Logic
{
  public class Enemy : CharacterBase
  {
    public EnemyID ID { get; private set; }
    public AIBrain AI { get; private set; }
    
    [SerializeField] private NavMeshAgent _agent;
    
    [Header("")] 
    [SerializeField] private List<ZoneTrigger> _triggers;

    private ICoroutineRunner _coroutineRunner;
    private EnemyData _data;

    [Inject]
    private void Construct(ICoroutineRunner coroutineRunner) => _coroutineRunner = coroutineRunner;

    public void SetData(EnemyID id, EnemyData data)
    {
      ID = id;
      _data = data;
    }

    
    protected override void Init()
    {
      Dictionary<ZoneTriggerID, ZoneTrigger> triggers = _triggers.ToDictionary(x => x.ID, x => x);

      Health = new EnemyHealth(_data.Life);
      Death = new EnemyDeath();
      Attack = new EnemyAttack(this, _data.Combat);
      Movement = new EnemyMovement(this, _data.Movement, _agent);
      Animator = new EnemyAnimator(this, p_Animator);
      AI = new AIBrain(this, _data.Actions, triggers, _coroutineRunner);
    }


    protected override void Tick()
    {
      if (Death.IsDead) return;
      
      AI.Tick();
      base.Tick();
    }

    protected override void OnDeath()
    {
      base.OnDeath();
      _agent.enabled = false;
    }
  }
}