using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public static class InputManager
{
    public enum MoveDirection
    {
        none,
        left = -1,
        right = 1
    }

    public enum VerticalDirection
    {
        none,
        crouch = -1,
        jump = 1
    }

    public static MoveDirection GetMoveDirection()
    {
        switch (Input.GetAxisRaw("Horizontal"))
        {
            case -1:
                return MoveDirection.left;
            case 1:
                return MoveDirection.right;
            default:
                return MoveDirection.none;
        }
    }

    public static VerticalDirection VerticalMove() //rename
    {
        switch (Input.GetAxisRaw("Vertical"))
        {
            case -1:
                return VerticalDirection.crouch;
            case 1:
                return VerticalDirection.jump;
            default:
                return VerticalDirection.none;
        }
    }

    public static bool Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
