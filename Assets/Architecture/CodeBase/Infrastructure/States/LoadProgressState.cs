﻿using CodeBase.Data;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.Infrastructure.States
{
  public class LoadProgressState : IDefaultState
  {
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
      _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.LevelPosition.LevelName);
    }

    public void Exit()
    {
      
    }

    private void LoadProgressOrInitNew() => 
      _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

    private PlayerProgress NewProgress() => 
      new PlayerProgress(startLevel: "Level_1");
  }
}