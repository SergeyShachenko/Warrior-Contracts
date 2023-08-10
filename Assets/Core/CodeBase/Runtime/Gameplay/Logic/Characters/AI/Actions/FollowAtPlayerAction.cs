using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WC.Runtime.Gameplay.Tools;
using WC.Runtime.Infrastructure.Services;

namespace WC.Runtime.Gameplay.Logic
{
  public class FollowAtPlayerAction : AIActionBase
  {
    private const float VisionRadiusIncrement = 5f;

    private readonly CharacterBase _character;
    private readonly ZoneTrigger _visionTrigger;
    private readonly ICoroutineRunner _coroutineRunner;

    private readonly WaitForSeconds _deleteTargetDelay = new(2f);
    private readonly Vector3 _startPosition;

    private CharacterBase _player;
    private Coroutine _deleteTargetCoroutine;

    public FollowAtPlayerAction(
      CharacterBase character,
      IReadOnlyDictionary<ZoneTriggerID, ZoneTrigger> triggers,
      ICoroutineRunner coroutineRunner)
    {
      _character = character;
      _coroutineRunner = coroutineRunner;
      _startPosition = _character.transform.position;

      _visionTrigger = triggers[ZoneTriggerID.Vision];
    }


    protected override void SubscribeUpdates()
    {
      _visionTrigger.TriggerEnter += OnVisionTriggerEnter;
      _visionTrigger.TriggerExit += OnVisionTriggerExit;
    }

    protected override void UnsubscribeUpdates()
    {
      _visionTrigger.TriggerEnter -= OnVisionTriggerEnter;
      _visionTrigger.TriggerExit -= OnVisionTriggerExit;
    }


    public override void Tick()
    {
      if (_player != null && _player.Death.IsDead == false)
      {
        _character.Movement.Move(_player.transform.position, MovementState.Run);
      }
      else
      {
        _character.Movement.Move(_startPosition, MovementState.Run);
      }
    }
    
    
    private void SetTarget(CharacterBase character)
    {
      _player = character;
      _visionTrigger.Radius += VisionRadiusIncrement;
    }

    private IEnumerator DeleteTarget()
    {
      yield return _deleteTargetDelay;

      _player = null;
      _visionTrigger.Radius -= VisionRadiusIncrement;
    }

    private void StartDeleteTarget() => _deleteTargetCoroutine = _coroutineRunner.StartCoroutine(DeleteTarget());

    private void StopDeleteTarget()
    {
      _coroutineRunner.StopCoroutine(_deleteTargetCoroutine);
      _deleteTargetCoroutine = null;
    }

    private void OnVisionTriggerEnter(Collider target)
    {
      if (target.TryGetComponent(out Player player) && player.Death.IsDead == false)
      {
        if (_player == null)
          SetTarget(player);

        if (_deleteTargetCoroutine != null)
          StopDeleteTarget();
      }
    }

    private void OnVisionTriggerExit(Collider target)
    {
      if (target.TryGetComponent(out Player _))
      {
        if (_deleteTargetCoroutine != null)
          StopDeleteTarget();

        StartDeleteTarget();
      }
    }
  }
}