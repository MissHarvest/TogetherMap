using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownLookMouseSide : MonoBehaviour
{
    private PlayerInputController _controller;

    [SerializeField] private SpriteRenderer characterSpriteRenderer;

    private void Awake()
    {
        _controller = GetComponent<PlayerInputController>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        _controller.OnLookEvenet += Look;
    }

    public void Look(Vector2 direction)
    {
        bool bLeft = transform.position.x - direction.x < 0;
        if(characterSpriteRenderer.flipX != bLeft)
        {
            //characterSpriteRenderer.flipX = bLeft;
            GameManager.Instance.Client.SendMsg($"look:{GameManager.Instance.UID},{bLeft}");
        }        
    }

    public void LookLeft(bool bLeft)
    {
        characterSpriteRenderer.flipX = bLeft;
    }
}
