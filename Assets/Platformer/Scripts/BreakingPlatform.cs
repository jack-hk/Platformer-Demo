using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingPlatform : MonoBehaviour
{
    private Rigidbody2D[] fragPhysics;
    private Animation[] fragAnimation;
    private Collider2D platformCollider;

    private void Awake()
    {
        fragPhysics = GetComponentsInChildren<Rigidbody2D>();
        fragAnimation = GetComponentsInChildren<Animation>();
        platformCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerTagManager.ContainsPlayerTag(collision.collider))
        {
            foreach (Animation anim in fragAnimation)
            {
                anim.Play("A_Platform_Breaking");
            }
            StartCoroutine(PlatformBreak());
        }
    }

    IEnumerator PlatformBreak()
    {
        yield return new WaitForSeconds(2);
        Destroy(platformCollider);
        foreach (Rigidbody2D body in fragPhysics)
        {
            body.bodyType = RigidbodyType2D.Dynamic;
            body.velocity = new Vector2(Random.Range(-3, 3), Random.Range(-3, 3));
        }
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
