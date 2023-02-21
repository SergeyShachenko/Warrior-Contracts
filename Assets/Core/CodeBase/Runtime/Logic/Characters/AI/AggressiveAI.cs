﻿using System.Collections;
using CodeBase.Logic.Tools;
using UnityEngine;

namespace CodeBase.Logic.Characters
{
  public class AggressiveAI : MonoBehaviour
  {
    [SerializeField] private float _cooldown = 3f;
    
    [Header("Links")]
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private FollowAIBase _followAI;
    
    private Coroutine _setMoveToPlayerOperation;
    private bool _hasTargetForAggressive;


    private void Start()
    {
      _triggerObserver.TriggerEnter += OnObserverTriggerEnter;
      _triggerObserver.TriggerExit += OnObserverTriggerExit;

      _followAI.enabled = false;
    }

    
    private void OnObserverTriggerEnter(Collider obj)
    {
      if (_hasTargetForAggressive) return;


      _hasTargetForAggressive = true;
      
      if (_setMoveToPlayerOperation != null)
      {
        StopCoroutine(_setMoveToPlayerOperation);
        _setMoveToPlayerOperation = null;
      }
      
      _followAI.enabled = true;
    }

    private void OnObserverTriggerExit(Collider obj)
    {
      if (_hasTargetForAggressive == false) return;

      _hasTargetForAggressive = false;
      _setMoveToPlayerOperation = StartCoroutine(SetMoveToPlayerAfterCooldown(isActive: false));
    }

    private IEnumerator SetMoveToPlayerAfterCooldown(bool isActive)
    {
      yield return new WaitForSeconds(_cooldown);
      
      _followAI.enabled = isActive;
    }
  }
}