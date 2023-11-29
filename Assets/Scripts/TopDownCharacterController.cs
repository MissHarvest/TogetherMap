using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvenet;
    public event Action OnAttackEvent;
    public event Action OnJumpEvent;

    private float _timeSinceLastAttack = float.MaxValue;
    protected bool IsAttacking { get; set; }
    protected bool IsJumping { get; set; }

    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvenet(Vector2 direction)
    {
        OnLookEvenet?.Invoke(direction);
    }

    private void HandleAttackDelay()
    {
        if(_timeSinceLastAttack <= 2.0f)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        
        if(IsAttacking && _timeSinceLastAttack > 0.2f)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent();
        }
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }

    public void CallJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }
}
