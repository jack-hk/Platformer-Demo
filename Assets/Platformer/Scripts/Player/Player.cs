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
    InputManager.MoveDirection LastDirection;

    Rigidbody2D playerPhysics;
    CapsuleCollider2D playerCollider;
    SpriteRenderer playerSprite;

    [SerializeField] private float speed;

    private void Start()
    {
        playerPhysics = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (IsAwake())
        {
            Move();
        }
    }

    private void Update()
    {
        if (IsAwake())
        {
           Action();
        }
    }

    private void Idle() 
    {
        Debug.Log("idle");
    }

    private void Move()
    {
        InputManager.Walk();
        //LastDirection = InputManager.Walk();
        playerPhysics.velocity = new Vector2((int)InputManager.Walk() * (speed * 10) * Time.deltaTime, playerPhysics.velocity.y);
    }

    private void Action()
    {
        if (InputManager.Attack())
        {

        }
    }
}
