using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Gravity Settings")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    private float mobileInputX = 0f;
    private bool jumpRequest = false;

    private bool isUIControlActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isUIControlActive)
        {
            mobileInputX = Input.GetAxisRaw("Horizontal");

            if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.01f)
            {
                jumpRequest = true;
            }
        }

        // Gravity adjustment
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        UpdateAnimation(mobileInputX);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(mobileInputX * moveSpeed, rb.velocity.y);

        if (jumpRequest && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRequest = false;
        }
    }

    void UpdateAnimation(float moveInput)
    {
        if (rb.velocity.y > 0.1f)
        {
            animator.Play("Jump");
        }
        else if (rb.velocity.y < -0.1f)
        {
            animator.Play("Fall");
        }
        else if (Mathf.Abs(moveInput) > 0.1f)
        {
            animator.Play("Run");
        }
        else
        {
            animator.Play("Idle");
        }

        if (moveInput > 0) sprite.flipX = false;
        else if (moveInput < 0) sprite.flipX = true;
    }

    // Dipanggil dari UI button dengan parameter isPressed
    public void MoveLeft(bool isPressed)
    {
        if (isPressed)
        {
            isUIControlActive = true;
            mobileInputX = -1f;
        }
        else
        {
            mobileInputX = 0f;
            isUIControlActive = false;
        }
    }

    public void MoveRight(bool isPressed)
    {
        if (isPressed)
        {
            isUIControlActive = true;
            mobileInputX = 1f;
        }
        else
        {
            mobileInputX = 0f;
            isUIControlActive = false;
        }
    }

    public void JumpButton(bool isPressed)
    {
        if (isPressed && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            jumpRequest = true;
        }
    }
}
