using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class Entity : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;

    #region Built-in
    private void Awake()
    {
        OnSpawned();
    }
    #endregion
    #region Custom
    public void OnSpawned()
    {
        health = maxHealth;
    }

    public bool IsAwake()
    {
        return true;
    }

    public bool IsDead()
    {
        if (health < 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
