using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//JackHK

public class Boomerang : Projectile
{
    private bool isReturning = false;

    Transform boomerangTransform;
    Transform playerTransform;
    SpriteRenderer boomerangSprite;


    protected override void Awake()
    {
        base.Awake();
        boomerangTransform = GetComponent<Transform>();
        playerTransform = GetComponentInParent<Transform>();
        boomerangSprite = GetComponent<SpriteRenderer>();
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
        projectilePhysics.velocity = new Vector2(0, projectilePhysics.velocity.y);
        projectilePhysics.transform.position = Vector2.MoveTowards(projectilePhysics.transform.position, playerTransform.transform.position, (speed * 10) * Time.fixedDeltaTime);
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
