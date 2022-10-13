using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D projectilePhysics;

    protected Vector2 startPosition;
    private bool isFiring = false;

    [Header("Configuration")]
    [SerializeField] protected int speed;
    [SerializeField] protected float despawnTimer;

    #region Built-in
    protected virtual void Awake()
    {
        projectilePhysics = GetComponent<Rigidbody2D>();
        startPosition = GetComponentInParent<Transform>().localPosition;
    }
    #endregion
    #region Custom
    public virtual void Fire(bool hasDespawn)
    {
        if (!this.gameObject.activeInHierarchy && isFiring == false)
        {
            isFiring = true;
            this.gameObject.SetActive(true);
        }
        Event();
        Despawn(hasDespawn);
    }
    protected virtual void Disable()
    {
        isFiring = false;
        this.gameObject.SetActive(false);
        this.gameObject.transform.localPosition = startPosition;
    }

    protected virtual bool Event()
    {
        return true;
    }

    protected virtual void Despawn(bool enabled)
    {
        isFiring = false;
        if (enabled)
        {
            StartCoroutine(DespawnTimer());
        }
    }

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTimer);
        Disable();
    }
    #endregion
}
