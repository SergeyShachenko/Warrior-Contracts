using System;
using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.AssetManagement;
using Zenject;

namespace WC.Runtime.Infrastructure.Services
{
  public class LoadProgressState : IDefaultState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;
    
    private Action _onExit;

    public LoadProgressState(GameStateMachine stateMachine, DiContainer container)
    {
      _stateMachine = stateMachine;
      _progressService = container.Resolve<IPersistentProgressService>();
      _saveLoadService = container.Resolve<ISaveLoadService>();
    }


    public void Enter(Action onExit = null)
    {
      _onExit = onExit;
      
      InitProgress();
      _stateMachine.Enter<LoadLevelState, string>(_progressService.Player.World.LevelPos.LevelName);
    }

    public void Exit() => 
      _onExit?.Invoke();

    private void InitProgress() => 
      _progressService.Player = _saveLoadService.LoadProgress() ?? NewProgress();

    private PlayerProgressData NewProgress()
    {
      var progress = new PlayerProgressData(AssetName.Scene.Level.Flat1);
      FillState(progress);
      FillStats(progress);

      return progress;
    }

    private void FillState(PlayerProgressData progress)
    {
      progress.State.MaxHP = 50f;
      progress.State.ResetHP();
    }

    private void FillStats(PlayerProgressData progress)
    {
      progress.Stats.Damage = 5f;
      progress.Stats.AttackDistance = 2f;
      progress.Stats.Cooldown = 1f;
      progress.Stats.HitRadius = 2f;
      progress.Stats.MovementSpeed = 8f;
    }
  }
}