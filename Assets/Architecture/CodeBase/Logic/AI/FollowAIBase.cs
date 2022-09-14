using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Logic.AI
{
  public abstract class FollowAIBase : MonoBehaviour
  {
    protected GameObject p_hero;
    protected IGameFactory p_gameFactory;
    
    
    private void Start()
    {
      p_gameFactory = AllServices.Container.Single<IGameFactory>();

      if (p_gameFactory.Hero != null)
        LinkHero();
      else
        p_gameFactory.HeroCreate += OnHeroCreate;
    }

    
    protected bool HeroIsCreated() => 
      p_hero != null;

    private void LinkHero() => 
      p_hero = p_gameFactory.Hero;
    
    private void OnHeroCreate() => 
      LinkHero();
  }
}