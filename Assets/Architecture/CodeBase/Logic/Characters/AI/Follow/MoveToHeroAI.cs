using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Logic.Characters
{
  public class MoveToHeroAI : FollowAIBase
  {
    [SerializeField] private float _minDistanceToHero = 2f;
    
    [Header("Links")]
    [SerializeField] private NavMeshAgent _agent;
    
    
    private void Update()
    {
      if (HeroIsCreated() && HeroIsFar() && p_HeroDeath.IsDead == false && p_EnemyDeath.IsDead == false)
        _agent.destination = p_Hero.transform.position;
    }
    

    private bool HeroIsFar() => 
      Vector3.Distance(_agent.transform.position, p_Hero.transform.position) >= _minDistanceToHero;
  }
}