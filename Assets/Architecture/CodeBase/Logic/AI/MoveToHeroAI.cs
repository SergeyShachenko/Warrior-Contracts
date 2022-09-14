using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Logic.AI
{
  public class MoveToHeroAI : FollowAIBase
  {
    private const float MinDistanceToPlayer = 1;
    
    [SerializeField] private NavMeshAgent _agent;
    
    
    private void Update()
    {
      if (HeroIsCreated() && HeroIsFar())
        _agent.destination = p_hero.transform.position;
    }
    

    private bool HeroIsFar() => 
      Vector3.Distance(_agent.transform.position, p_hero.transform.position) >= MinDistanceToPlayer;
  }
}