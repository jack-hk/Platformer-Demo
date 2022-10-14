using System.Collections;
using UnityEngine;

//JackHK

public class Player : Entity
{
    [Header("Abilites")]
    public GameObject rangedProjectile;
    [HideInInspector] public float jetpackFuel;
    public float jetpackFuelMax;
    public bool isClimbing = false;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float climbSpeed;


    private Rigidbody2D playerPhysics;
    private CapsuleCollider2D playerCollider;
    private SpriteRenderer playerSprite;
    private Animator animator;
    private Boomerang boomerang;
    private ParticleSystem groundParticle;
    private GameObject healthBar, fuelBar;

    private UIManager playerUI;
    private bool dustCooldown = true;
    private bool healthUICooldown = false;
    private bool fuelUICooldown = false;
    
    private float oldHealth, oldFuel;

    #region Built-in
    protected void Awake()
    {
        playerPhysics = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        groundParticle = GetComponentInChildren<ParticleSystem>();
        playerUI = GetComponentInChildren<UIManager>();

        healthBar = GameObject.Find("Health");
        fuelBar = GameObject.Find("Fuel");

        boomerang = rangedProjectile.GetComponent<Boomerang>();
    }

    protected override void Start()
    {
        base.Start();
        jetpackFuel = jetpackFuelMax;
        StartCoroutine(UIHealthFade());
        StartCoroutine(UIFuelFade());
    }

    private void FixedUpdate()
    {
        if (IsAwake())
        {
            SpecialAction();
            Walk();
        }
    }

    private void Update()
    {
        DeathCheck();
        UIUpdate();
        DustEffect();
        ResetClimbState();
        if (IsAwake())
        {
            AttackAction();
            Jump();
        }
    }
    #endregion
    #region Custom

    private void DeathCheck()
    {
        if (IsDead())
        {
            playerPhysics.velocity = new Vector2(0, -1);
            playerCollider.isTrigger = true;
            StartCoroutine(DeathAnimation());
        }
    }

    #region Movement
    private void Walk()
    {
        switch (InputManager.GetHorizontalInput())
        {
            case InputManager.HorizontalDirection.right:
                animator.SetInteger("horizontalMovement", 1);
                playerSprite.flipX = true; //fix at later date, needs reversing
                break;
            case InputManager.HorizontalDirection.left:
                animator.SetInteger("horizontalMovement", 1);
                playerSprite.flipX = false;
                break;
            default:
                animator.SetInteger("horizontalMovement", 0);
                break;
        }
        playerPhysics.velocity = new Vector2((int)InputManager.GetHorizontalInput() * (moveSpeed * 10) * Time.deltaTime, playerPhysics.velocity.y);
    }

    private void Jump()
    {
        if (playerPhysics.velocity.y <= 0)
        {
            animator.SetBool("isFalling", !IsGrounded());
        }
        animator.SetBool("isJumping", !IsGrounded());

        switch (InputManager.GetVerticalInput())
        {
            case InputManager.VerticalDirection.crouch:
                //play crouch anim
                break;
            case InputManager.VerticalDirection.jump:
                if (IsGrounded() && !isClimbing)
                {
                    playerPhysics.velocity = Vector2.up * jumpHeight;
                }
                else if (isClimbing)
                {
                    Climb();
                }
                break;
            default:
                break;
        }
    }

    private void Climb()
    {
        playerPhysics.velocity = Vector2.up * climbSpeed;
        animator.SetBool("isClimbing", true);
    }

    private void ResetClimbState()
    {
        if (!isClimbing)
        {
            animator.SetBool("isClimbing", false);
        }
    }

    private void SpecialAction()
    {
        if (InputManager.GetAbilityInput())
        {
            if (jetpackFuel > 0)
            {
                jetpackFuel = jetpackFuel - 1;
                playerPhysics.velocity = Vector2.up * jumpHeight / 2;
            }
            else
            {
                jetpackFuel = 0;
            }
            
        }
    }

    private bool IsGrounded()
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
        return rayhit.collider != null;
    }
    
    private void AttackAction() 
    {
        if (InputManager.GetRangedInput())
        {
            if (!boomerang.isActiveAndEnabled)
            {
                boomerang.Fire(true);
                animator.SetBool("isRangedAttacking", true);
                StartCoroutine(AttackAnimation());
            }
        }
    }
    #endregion

    private void DustEffect()
    {

        if (IsGrounded() && dustCooldown == false)
        {
            groundParticle.Play();
            dustCooldown = true;
        }
        if (!IsGrounded())
        {
            dustCooldown = false;
        }
    }

    private void UIUpdate()
    {
        playerUI.healthUI.transform.localScale = new Vector2(health / maxHealth, playerUI.healthUI.transform.localScale.y);
        playerUI.fuelUI.transform.localScale = new Vector2(jetpackFuel / jetpackFuelMax, playerUI.fuelUI.transform.localScale.y);

        if (health != oldHealth && healthUICooldown)
        {
            healthUICooldown = false;
            healthBar.gameObject.SetActive(true);
            StartCoroutine(UIHealthFade());
        }

        if (jetpackFuel != oldFuel && fuelUICooldown)
        {
            fuelUICooldown = false;
            fuelBar.gameObject.SetActive(true);
            StartCoroutine(UIFuelFade());
        }
        oldHealth = health;
        oldFuel = jetpackFuel;
    }

    IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("isRangedAttacking", false);
    }

    IEnumerator UIHealthFade()
    {
        yield return new WaitForSeconds(3);
        healthBar.gameObject.SetActive(false);
        healthUICooldown = true;
    }

    IEnumerator UIFuelFade()
    {
        yield return new WaitForSeconds(5);
        fuelBar.gameObject.SetActive(false);
        fuelUICooldown = true;
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
    #endregion
}
