using UnityEngine;
using UnityEngine.AI;
using WC.Runtime.Logic.Characters;

namespace WC.Runtime.Logic.Animation
{
  public class AnimateAlongAgent : MonoBehaviour
  {
    private const float MinAgentSpeed = 0.1f;
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Enemy _enemy;
    
    private EnemyAnimator _enemyAnimator;


    private void Awake() => 
      _enemy.Initialized += OnEnemyInit;

    private void Update()
    {
      if (AgentShouldMove())
        _enemyAnimator.Move(_agent.velocity.magnitude);
      else
        _enemyAnimator.StopMove();
    }

    
    private void OnEnemyInit() => 
      _enemyAnimator = (EnemyAnimator)_enemy.Animator;

    private bool AgentShouldMove() => 
      _agent.velocity.magnitude > MinAgentSpeed && _agent.remainingDistance > _agent.radius;
  }
}