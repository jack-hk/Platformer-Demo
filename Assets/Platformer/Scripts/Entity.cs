using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class Entity : MonoBehaviour
{
    [HideInInspector] public float health;
    [SerializeField] protected float maxHealth;

    #region Built-in
    protected virtual void Start()
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
        if (IsDead())
        {
            return false;
        } 
        else return true;
    }

    public bool IsDead()
    {
        return health < 1;
    }
    #endregion
}
