using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D projectilePhysics;

    protected Vector2 startPosition;

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
        if (!this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(true);
        }
        Event();
        Despawn(hasDespawn);
    }
    protected void Disable()
    {
        this.gameObject.SetActive(false);
        this.gameObject.transform.localPosition = startPosition; ;
    }

    protected virtual bool Event()
    {
        return true;
    }

    private void Despawn(bool enabled)
    {
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
