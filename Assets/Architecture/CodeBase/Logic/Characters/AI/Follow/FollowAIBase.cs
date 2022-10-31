using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using CodeBase.Logic.Characters.Enemy;
using CodeBase.Logic.Characters.Hero;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public abstract class FollowAIBase : MonoBehaviour
  {
    [Header("Links")]
    [SerializeField] protected EnemyDeath p_EnemyDeath;
    
    protected GameObject p_Hero;
    protected IGameFactory p_GameFactory;
    protected HeroDeath p_HeroDeath;


    private void Start()
    {
      p_GameFactory = AllServices.Container.Single<IGameFactory>();

      if (p_GameFactory.Hero != null)
        LinkHero();
      else
        p_GameFactory.HeroCreate += OnHeroCreate;
    }

    
    protected bool HeroIsCreated() => 
      p_Hero != null;

    private void LinkHero()
    {
      p_Hero = p_GameFactory.Hero;
      p_HeroDeath = p_Hero.GetComponent<HeroDeath>();
    }

    private void OnHeroCreate() => 
      LinkHero();
  }
}