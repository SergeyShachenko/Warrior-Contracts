using System;
using System.Collections.Generic;
using WC.Runtime.Gameplay.Tools;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Gameplay.Logic
{
  public class AIBrain : ILogicComponent
  {
    public event Action Changed;

    public bool IsActive { get; set; } = true;

    public IReadOnlyList<AIActionBase> ActiveActions => new List<AIActionBase>(_activeActions);

    private readonly CharacterBase _character;

    private Dictionary<AIActionID, AIActionBase> _actionsData;
    private readonly List<AIActionBase> _activeActions = new();

    public AIBrain(CharacterBase character,
      IEnumerable<AIActionID> actionsIDs,
      Dictionary<ZoneTriggerID, ZoneTrigger> triggers, 
      ICoroutineRunner coroutineRunner)
    {
      _character = character;

      InitActionsData(triggers, coroutineRunner);
      FillActiveActions(actionsIDs);
    }


    public void Tick()
    {
      if (IsActive == false) return;
      
      foreach (AIActionBase action in _activeActions) 
        action.Tick();
    }


    public void Add(AIActionID id)
    {
      SilentAdd(id);
      Changed?.Invoke();
    }

    public void Add(IEnumerable<AIActionID> ids)
    {
      foreach (AIActionID id in ids)
        Add(id);
    }
    
    public void Remove(AIActionID id)
    {
      SilentRemove(id);
      Changed?.Invoke();
    }

    public void Remove(IEnumerable<AIActionID> ids)
    {
      foreach (AIActionID id in ids)
        Remove(id);
    }
    
    
    private void InitActionsData(Dictionary<ZoneTriggerID, ZoneTrigger> triggers, ICoroutineRunner coroutineRunner)
    {
      _actionsData = new Dictionary<AIActionID, AIActionBase>
      {
        {AIActionID.None, new NoneAction()},
        {AIActionID.Patrol, new PatrolAction(_character)},
        {AIActionID.FollowAtPlayer, new FollowAtPlayerAction(_character, triggers, coroutineRunner)},
        {AIActionID.AttackAtPlayer, new AttackAtPlayerAction(_character, triggers)}
      };
    }
    
    private void FillActiveActions(IEnumerable<AIActionID> actionsIDs)
    {
      foreach (AIActionID id in actionsIDs) 
        SilentAdd(id);
    }
    
    private void SilentAdd(AIActionID id)
    {
      AIActionBase action = _actionsData[id];

      _activeActions.Add(action);
      _actionsData[id].Init();
    }
    
    private void SilentRemove(AIActionID id)
    {
      AIActionBase action = _actionsData[id];

      _activeActions.Remove(action);
      action.Dispose();
    }
  }
}