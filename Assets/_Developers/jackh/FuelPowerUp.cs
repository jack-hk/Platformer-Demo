using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPowerUp : MonoBehaviour
{
    private Collider2D colliderObj;
    private ParticleSystem particle;
    private SpriteRenderer sprite;

    private void Awake()
    {
        colliderObj = GetComponent<Collider2D>();
        particle = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerTagManager.ContainsPlayerTag(collision.collider))
        {
            Player player;
            player = collision.collider.GetComponent<Player>();
            player.jetpackFuel = player.jetpackFuelMax;
            StartCoroutine(Respawn());
            colliderObj.enabled = false;
            sprite.enabled = false;
            particle.Stop();
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5);
        colliderObj.enabled = true;
        sprite.enabled = true;
        particle.Play();
    }
}
