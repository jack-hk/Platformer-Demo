using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int damageValue;
    private bool cooldownCheck = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerTagManager.ContainsPlayerTag(collision.collider) && cooldownCheck == false)
        {
            Player player;
            player = collision.collider.GetComponent<Player>();
            player.health = player.health - damageValue;
            cooldownCheck = true;
            StartCoroutine(DamageCoolDown());
        }
    }

    IEnumerator DamageCoolDown()
    {
        yield return new WaitForSeconds(1);
        cooldownCheck = false;
    }
}
