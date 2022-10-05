using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//JackHK

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D projectilePhysics;

    private Vector3 startPosition;

    [SerializeField] protected int speed;
    [SerializeField] protected float despawnTimer;
    private bool isFired = false;

    protected virtual void Awake()
    {
        projectilePhysics = GetComponent<Rigidbody2D>();
        startPosition = GetComponentInParent<Transform>().localPosition;
    }

    public virtual void Fire(bool hasDespawn)
    {
        if (!isFired)
        {
            isFired = true;
            if (!this.gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(true);
            }
            Event();
            Despawn(hasDespawn);
            isFired = false;
        }
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
        this.gameObject.SetActive(false);
        this.gameObject.transform.localPosition = startPosition;
    }
}
