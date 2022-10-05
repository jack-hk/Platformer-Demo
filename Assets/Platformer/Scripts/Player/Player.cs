using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Windows;

//JackHK

public class Player : Entity
{
    Rigidbody2D playerPhysics;
    CapsuleCollider2D playerCollider;
    SpriteRenderer playerSprite;
    Boomerang boomerang;
    
    [SerializeField] private float speed;

    public GameObject rangedProjectile;

    private void Awake()
    {
        playerPhysics = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        boomerang = rangedProjectile.GetComponent<Boomerang>();
    }

    private void FixedUpdate()
    {
        if (IsAwake())
        {
            Walk();
        }
    }

    private void Update()
    {
        if (IsAwake())
        {
            Action();
        }
    }

    private void Walk()
    {
        switch (InputManager.GetMoveDirection())
        {
            case InputManager.MoveDirection.right:
                playerSprite.flipX = false;
                break;
            case InputManager.MoveDirection.left:
                playerSprite.flipX = true;
                break;
            default:
                break;
        }
        playerPhysics.velocity = new Vector2((int)InputManager.GetMoveDirection() * (speed * 10) * Time.deltaTime, playerPhysics.velocity.y);
    }
    
    private void Action()
    {
        if (InputManager.Attack())
        {
            boomerang.Fire(false);
        }
    }
}
