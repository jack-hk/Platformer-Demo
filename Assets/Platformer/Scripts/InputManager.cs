using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public static class InputManager
{
    public enum HorizontalDirection
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

    public static HorizontalDirection GetHorizontalInput()
    {
        switch (Input.GetAxisRaw("Horizontal"))
        {
            case -1:
                return HorizontalDirection.left;
            case 1:
                return HorizontalDirection.right;
            default:
                return HorizontalDirection.none;
        }
    }

    public static VerticalDirection GetVerticalInput()
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

    public static bool GetAbilityInput()
    {
        if (Input.GetButton("Jump"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool GetRangedInput()
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
