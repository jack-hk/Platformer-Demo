using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//JackHK

public class Boomerang : Projectile
{
    private SpriteRenderer boomerangSprite;
    private Collider2D boomerangCollider, playerCollider;
    private Animator playerAnimator;
    private bool isReturning;
    private bool rangedCheck = false;
    private int lastRecordedDirection = 1;

    [SerializeField] private float range;

    private Vector2 playerPosition;


    #region Built-in
    protected override void Awake()
    {
        base.Awake();
        boomerangSprite = GetComponent<SpriteRenderer>();
        boomerangCollider = GetComponent<Collider2D>();
        playerCollider = GetComponentInParent<Collider2D>();
        playerAnimator = GetComponentInParent<Animator>();
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (LayerTagManager.ContainsPlayerTag(collider) && isReturning == true)
        {
            Disable();
        }
    }
    #endregion
    #region Custom
    public override void Fire(bool hasDespawn)
    {
        base.Fire(hasDespawn);
        isReturning = false;
        rangedCheck = false;
    }

    protected override bool Event()
    {
        StartCoroutine(EventTimer());
        return true;
    }

    protected override void Disable()
    {
        base.Disable();
        
    }

    protected override void Despawn(bool enabled)
    {
        base.Despawn(enabled);
        
    }

    private int RangedDirection()
    {
        if (!rangedCheck)
        {
            if ((int)InputManager.GetHorizontalInput() == 0)
            {
                lastRecordedDirection = 1;
                rangedCheck = true;
                return 1;
            }
            else
            {
                lastRecordedDirection = (int)InputManager.GetHorizontalInput();
                rangedCheck = true;
                return (int)InputManager.GetHorizontalInput();
            }
        }
        else
            return lastRecordedDirection;
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
        isReturning = true;
    }
    #endregion

}
