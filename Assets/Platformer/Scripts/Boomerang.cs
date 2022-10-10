using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//JackHK

public class Boomerang : Projectile
{
    private SpriteRenderer boomerangSprite;
    private Collider2D boomerangCollider, playerCollider;

    [SerializeField] private float range;

    private Vector2 playerPosition;

    #region Built-in
    protected override void Awake()
    {
        base.Awake();
        boomerangSprite = GetComponent<SpriteRenderer>();
        boomerangCollider = GetComponent<Collider2D>();
        playerCollider = GetComponentInParent<Collider2D>();
    }

    private void Update()
    {
        TravelReturn();

    }

    private void FixedUpdate()
    {
        TravelOut();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (LayerManager.ContainsPlayerTag(collider))
        {
            Disable();
        }
    }
    #endregion
    #region Custom
    public override void Fire(bool hasDespawn)
    {
        base.Fire(hasDespawn);
        //fire event code goes here
        boomerangSprite.color = Color.red;
    }

    protected override bool Event()
    {
        StartCoroutine(EventTimer());
        return true;
    }

    private int RangedDirection()
    {
        if ((int)InputManager.GetHorizontalInput() == 0)
        {
            return 1;
        }
        else
        {
            return (int)InputManager.GetHorizontalInput();
        }

    }

    private void TravelOut()
    {

        projectilePhysics.velocity = new Vector2(((speed * 10) * Time.deltaTime * RangedDirection()), projectilePhysics.velocity.y);
    }

    private void TravelReturn()
    {
        playerPosition = gameObject.transform.parent.position;
        projectilePhysics.velocity = Vector2.zero;
        projectilePhysics.transform.position = Vector2.MoveTowards(projectilePhysics.transform.position, playerPosition, (speed / 5) * Time.deltaTime);
    }

    IEnumerator EventTimer()
    {
        yield return new WaitForSeconds(range);
        boomerangSprite.color = Color.blue;
    }
    #endregion

}
