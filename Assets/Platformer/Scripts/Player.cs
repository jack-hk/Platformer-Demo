using UnityEngine;

//JackHK

public class Player : Entity
{
    Rigidbody2D playerPhysics;
    CapsuleCollider2D playerCollider;
    SpriteRenderer playerSprite;
    Boomerang boomerang;

    [Header("Configuration")]
    [SerializeField] private LayerMask surfaceLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jump;

    public GameObject rangedProjectile;

    private void Awake()
    {
        playerPhysics = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        boomerang = rangedProjectile.GetComponent<Boomerang>();
    }

    private void FixedUpdate()
    {
        if (IsAwake())
        {
            Walk();
        }
    }

    private void Update()
    {
        if (IsAwake())
        {
            AttackAction();
            Jump();
        }
    }

    private void Walk()
    {
        switch (InputManager.GetMoveDirection())
        {
            case InputManager.MoveDirection.right:
                playerSprite.flipX = false;
                break;
            case InputManager.MoveDirection.left:
                playerSprite.flipX = true;
                break;
            default:
                break;
        }
        playerPhysics.velocity = new Vector2((int)InputManager.GetMoveDirection() * (speed * 10) * Time.deltaTime, playerPhysics.velocity.y);
    }

    private void Jump()
    {
        float extraHeightTest = 0.3f; //additional raycast length for surface detection
        RaycastHit2D rayhit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeightTest, surfaceLayer);

#if UNITY_EDITOR //visual ray for debugging
        Color rayColor;
        if (rayhit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + extraHeightTest), rayColor);
#endif

        switch (InputManager.VerticalMove())
        {
            case InputManager.VerticalDirection.crouch:
                //play crouch anim
                break;
            case InputManager.VerticalDirection.jump:
                if (IsGrounded(rayhit))
                {
                    playerPhysics.velocity = Vector2.up * jump;
                }
                break;
            default:
                break;
        }
    }

    private bool IsGrounded(RaycastHit2D ray)
    {

        return ray.collider != null;
    }

    private void AttackAction()
    {
        if (InputManager.Attack())
        {
            boomerang.Fire(true);
        }
    }


}
