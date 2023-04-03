using System;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameLoopState : PayloadGameStateBase<DiContainer>
  {
    private readonly ISaveLoadRegistry _saveLoadRegistry;
    private readonly ISaveLoadService _saveLoadService;

    private ICharacterRegistry _characterRegistry;
    private IUIRegistry _uiRegistry;

    private ICharacterFactory _characterFactory;
    private ILevelFactory _levelFactory;
    private ILootFactory _lootFactory;

    private IUIFactory _uiFactory;
    private IHUDFactory _hudFactory;

    public GameLoopState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container)
    {
      _saveLoadRegistry = container.Resolve<ISaveLoadRegistry>();
      _saveLoadService = container.Resolve<ISaveLoadService>();
    }


    public override void Enter(DiContainer subContainer, Action onExit = null)
    {
      BindSubServices(subContainer);

      _saveLoadService.SaveProgress();
      
      base.Enter(subContainer, onExit);
    }

    public override void Exit()
    {
      CleanUp();
      
      base.Exit();
    }

    
    private void BindSubServices(DiContainer subContainer)
    {
      _characterRegistry = subContainer.Resolve<ICharacterRegistry>();
      _uiRegistry = subContainer.Resolve<IUIRegistry>();

      _characterFactory = subContainer.Resolve<ICharacterFactory>();
      _levelFactory = subContainer.Resolve<ILevelFactory>();
      _lootFactory = subContainer.Resolve<ILootFactory>();
      
      _uiFactory = subContainer.Resolve<IUIFactory>();
      _hudFactory = subContainer.Resolve<IHUDFactory>();
    }

    private void CleanUp()
    {
      _saveLoadRegistry.CleanUp();
      _characterRegistry.CleanUp();
      _uiRegistry.CleanUp();
      
      _lootFactory.CleanUp();
      _characterFactory.CleanUp();
      _levelFactory.CleanUp();
      
      _uiFactory.CleanUp();
      _hudFactory.CleanUp();
    }
  }
}