using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Logic.AI
{
  public class AgentMoveToHero : MonoBehaviour
  {
    private const float MinDistanceToPlayer = 1;
    
    [SerializeField] private NavMeshAgent _agent;
    
    private GameObject _hero;
    private IGameFactory _gameFactory;


    private void Start()
    {
      _gameFactory = AllServices.Container.Single<IGameFactory>();

      if (_gameFactory.Hero != null)
        LinkHero();
      else
        _gameFactory.HeroCreate += OnHeroCreate;
    }
    
    private void Update()
    {
      if (HeroIsCreated() && HeroIsFar())
        _agent.destination = _hero.transform.position;
    }


    private void LinkHero() => 
      _hero = _gameFactory.Hero;

    private bool HeroIsCreated() => 
      _hero != null;

    private bool HeroIsFar() => 
      Vector3.Distance(_agent.transform.position, _hero.transform.position) >= MinDistanceToPlayer;

    private void OnHeroCreate() => 
      LinkHero();
  }
}