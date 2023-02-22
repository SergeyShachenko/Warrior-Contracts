using System;
using System.Collections.Generic;
using WC.Runtime.UI.Services;
using WC.Runtime.UI.Screens;
using WC.Runtime.Infrastructure.Services;
using WC.Runtime.UI;

namespace WC.Runtime.Infrastructure.Services
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IStateBase> _states;
    private IStateBase _currentState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices services)
    {
      _states = new Dictionary<Type, IStateBase>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
        
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen, 
          services.Single<IGameFactory>(), 
          services.Single<IPersistentProgressService>(), 
          services.Single<IStaticDataService>(),
          services.Single<IUIFactory>()),
        
        [typeof(LoadProgressState)] = new LoadProgressState(this, 
          services.Single<IPersistentProgressService>(), 
          services.Single<ISaveLoadService>()),
        
        [typeof(GameLoopState)] = new GameLoopState(this)
      };
    }
    
    
    public void Enter<TState>() where TState : class, IDefaultState => 
      SetCurrentState<TState>().Enter();

    public void Enter<TState, TParam>(TParam param) where TState : class, IPayloadState<TParam> => 
      SetCurrentState<TState>().Enter(param);

    private TState SetCurrentState<TState>() where TState : class, IStateBase
    {
      _currentState?.Exit();
      _currentState = GetState<TState>();
      
      return (TState) _currentState;
    }

    private TState GetState<TState>() where TState : class, IStateBase => 
      _states[typeof(TState)] as TState;
  }
}