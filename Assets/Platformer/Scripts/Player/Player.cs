using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Windows;

//JackHK

public class Player : Entity
{
    Vector2 playerVector;
    Rigidbody2D playerPhysics;
    CapsuleCollider2D playerCollider;
    SpriteRenderer playerSprite;


    Action MoveState, ActionState;

    protected int speed;

    private void Start()
    {
        playerPhysics = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();

    }

    private void FixedUpdate()
    {
        MoveState = IsAwake() ? Move : Idle;
    }

    private void Update()
    {
        ActionState = IsAwake() ? Action : Idle;
    }

    private void Idle() { }

    private void Move()
    {
        playerPhysics.velocity = new Vector2((int)InputManager.Walk() * 2 * Time.deltaTime, playerPhysics.velocity.y);
        
        switch (InputManager.Walk())
        {
            case InputManager.Direction.right:
                break;
            case InputManager.Direction.left:
                break;
            default:
                break;
        }
    }

    private void Action()
    {
        if (InputManager.Attack())
        {

        }
    }
}
