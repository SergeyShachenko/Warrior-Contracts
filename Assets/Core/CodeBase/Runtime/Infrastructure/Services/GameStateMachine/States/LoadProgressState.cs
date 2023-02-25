using WC.Runtime.Data.Characters;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Infrastructure.Services
{
  public class LoadProgressState : IDefaultState
  {
    private const string StartLevel = "Level_Flat_1";
    
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(
      GameStateMachine gameStateMachine,
      IPersistentProgressService progressService,
      ISaveLoadService saveLoadService)
    {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }

    
    public void Enter()
    {
      LoadProgressOrInitNew();
      _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.World.LevelPos.LevelName);
    }

    public void Exit()
    {
      
    }

    private void LoadProgressOrInitNew() => 
      _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

    private PlayerProgressData NewProgress()
    {
      var progress = new PlayerProgressData(StartLevel);
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