using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public static class InputManager
{
    public enum Direction
    {
        right,
        left
    }

    public static Direction Walk()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            return Direction.left;
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            return Direction.right;
        }
        return Direction.right;
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
