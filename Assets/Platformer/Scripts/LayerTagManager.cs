using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public static class LayerTagManager
{
    public static LayerMask ground = LayerMask.GetMask("Ground");

    public static bool ContainsPlayerTag(Collider2D collider)
    {
        if (collider.tag.Contains("Player"))
        {
            return true;
        }
        else return false;
        
    }
}
