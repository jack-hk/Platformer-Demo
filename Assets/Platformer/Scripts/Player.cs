using UnityEngine;

//JackHK

public class Player : Entity
{
    Rigidbody2D playerPhysics;
    CapsuleCollider2D playerCollider;
    SpriteRenderer playerSprite;
    Boomerang boomerang;

    [Header("Configuration")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;

    public GameObject rangedProjectile;

    #region Built-in
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
    #endregion
    #region Custom
    private void Walk()
    {
        switch (InputManager.GetHorizontalInput())
        {
            case InputManager.HorizontalDirection.right:
                playerSprite.flipX = false;
                break;
            case InputManager.HorizontalDirection.left:
                playerSprite.flipX = true;
                break;
            default:
                break;
        }
        playerPhysics.velocity = new Vector2((int)InputManager.GetHorizontalInput() * (moveSpeed * 10) * Time.deltaTime, playerPhysics.velocity.y);
    }

    private void Jump()
    {
        float extraHeightTest = 0.3f; //additional raycast length for surface detection
        RaycastHit2D rayhit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeightTest, LayerManager.ground);

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
            boomerang.Fire(true);
        }
    }
    #endregion

}
