using System;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameLoopState : PayloadGameStateBase<DiContainer>
  {
    private readonly ISaveLoadService _saveLoadService;
    private readonly IUIFactory _uiFactory;
    private readonly IUIRegistry _uiRegistry;

    private ICharacterFactory _characterFactory;
    private ILevelFactory _levelFactory;
    private ILootFactory _lootFactory;
    private IHUDFactory _hudFactory;

    public GameLoopState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container)
    {
      _saveLoadService = Container.Resolve<ISaveLoadService>();
      _uiFactory = Container.Resolve<IUIFactory>();
      _uiRegistry = Container.Resolve<IUIRegistry>();
    }


    public override void Enter(DiContainer subContainer, Action onExit = null)
    {
      BindSubServices(subContainer);

      _saveLoadService.SaveProgress();
      
      base.Enter(subContainer, onExit);
    }

    public override void Exit()
    {
      CleanUpServices();
      
      base.Exit();
    }

    
    private void BindSubServices(DiContainer subContainer)
    {
      _hudFactory = subContainer.Resolve<IHUDFactory>();
      _characterFactory = subContainer.Resolve<ICharacterFactory>();
      _levelFactory = subContainer.Resolve<ILevelFactory>();
      _lootFactory = subContainer.Resolve<ILootFactory>();
    }

    private void CleanUpServices()
    {
      _uiRegistry.CleanUp();
      _uiFactory.CleanUp();
      _hudFactory.CleanUp();
      _lootFactory.CleanUp();
      _characterFactory.CleanUp();
      _levelFactory.CleanUp();
    }
  }
}