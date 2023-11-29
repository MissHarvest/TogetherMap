using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownCharacterController
{
    public event Action OnChatActiveEvent;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main; 
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        string id = GameManager.Instance.UID;
        GameManager.Instance.Client.SendMsg($"move:{id},{moveInput.x},{moveInput.y}");
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        CallLookEvenet(worldPos);
    }

    public void OnFire(InputValue value)
    {
        //Debug.Log("OnFire" + value.ToString());
        IsAttacking = value.isPressed;
    }

    public void OnJump(InputValue value)
    {
        IsJumping = value.isPressed;
    }
    
    public void OnChat(InputValue value)
    {
        if(value.isPressed)
            OnChatActiveEvent?.Invoke();
    }
}
