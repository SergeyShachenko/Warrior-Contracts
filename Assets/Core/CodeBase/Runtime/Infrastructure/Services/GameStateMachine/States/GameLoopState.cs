using System;
using WC.Runtime.Gameplay.Services;
using WC.Runtime.UI.Services;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameLoopState : PayloadGameStateBase<DiContainer>
  {
    private readonly ISaveLoadService _saveLoadService;

    private IUIFactory _uiFactory;
    private IHUDFactory _hudFactory;
    private ICharacterFactory _characterFactory;
    private ILevelToolsFactory _levelToolsFactory;
    private ILootFactory _lootFactory;

    public GameLoopState(GameStateMachine stateMachine, DiContainer container) : base(stateMachine, container) => 
      _saveLoadService = container.Resolve<ISaveLoadService>();


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
      _uiFactory = subContainer.Resolve<IUIFactory>();
      _hudFactory = subContainer.Resolve<IHUDFactory>();
      _characterFactory = subContainer.Resolve<ICharacterFactory>();
      _levelToolsFactory = subContainer.Resolve<ILevelToolsFactory>();
      _lootFactory = subContainer.Resolve<ILootFactory>();
    }

    private void CleanUp()
    {
      _saveLoadService.CleanUp();

      _uiFactory.CleanUp();
      _hudFactory.CleanUp();
      _lootFactory.CleanUp();
      _characterFactory.CleanUp();
      _levelToolsFactory.CleanUp();
    }
  }
}