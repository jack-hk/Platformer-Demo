using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//JackHK

public class Boomerang : Projectile
{
    private bool isReturning = false;

    SpriteRenderer boomerangSprite;

    private Vector2 playerPosition;


    protected override void Awake()
    {
        base.Awake();
        boomerangSprite = GetComponent<SpriteRenderer>();;
    }

    private void Update()
    {
        if (isReturning)
        {
            TravelReturn();
        }
    }

    private void FixedUpdate()
    {
        if (!isReturning)
        {
            TravelOut();
        }
    }

    private void TravelOut()
    {

        projectilePhysics.velocity = new Vector2((speed * 10) * Time.deltaTime, projectilePhysics.velocity.y);
    }

    private void TravelReturn()
    {
        playerPosition = gameObject.transform.parent.position;
        projectilePhysics.velocity = Vector2.zero;
        projectilePhysics.transform.position = Vector2.MoveTowards(projectilePhysics.transform.position, playerPosition, (speed / 5) * Time.deltaTime);
    }

    public override void Fire(bool hasDespawn)
    {
        base.Fire(hasDespawn);
        isReturning = false;
        boomerangSprite.color = Color.red;
    }

    protected override bool Event()
    {
        StartCoroutine(EventTimer());
        return true;
    }

    IEnumerator EventTimer()
    {
        yield return new WaitForSeconds(despawnTimer / 2);
        boomerangSprite.color = Color.blue;
        isReturning = true;
    }
}