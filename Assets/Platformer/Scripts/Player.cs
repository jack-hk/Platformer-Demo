using UnityEngine;

//JackHK

public class Player : Entity
{
    private Rigidbody2D playerPhysics;
    private CapsuleCollider2D playerCollider;
    private SpriteRenderer playerSprite;
    private Boomerang boomerang;

    [Header("Configuration")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;

    public GameObject rangedProjectile;

    #region Built-in
    protected override void Awake()
    {
        base.Awake();
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
    #endregion
    #region Custom

    private void Walk()
    {
        switch (InputManager.GetHorizontalInput())
        {
            case InputManager.HorizontalDirection.right:
                playerSprite.flipX = true; //fix at later date, needs reversing
                break;
            case InputManager.HorizontalDirection.left:
                playerSprite.flipX = false;
                break;
            default:
                break;
        }
        playerPhysics.velocity = new Vector2((int)InputManager.GetHorizontalInput() * (moveSpeed * 10) * Time.deltaTime, playerPhysics.velocity.y);
    }

    private void Jump()
    {
        float extraHeightTest = 0.3f; //additional raycast length for surface detection
        RaycastHit2D rayhit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeightTest, LayerTagManager.ground);
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

        switch (InputManager.GetVerticalInput())
        {
            case InputManager.VerticalDirection.crouch:
                //play crouch anim
                break;
            case InputManager.VerticalDirection.jump:
                if (IsGrounded(rayhit))
                {
                    playerPhysics.velocity = Vector2.up * jumpHeight;
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
        if (InputManager.GetRangedInput())
        {
            if (!boomerang.isActiveAndEnabled)
            {
                boomerang.Fire(true);
            }
        }
    }
    #endregion

}
