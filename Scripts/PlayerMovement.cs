using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundPoundForce = 30f; // Triple normal fall speed
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.1f; // Distance for raycast
    [SerializeField] private LayerMask groundLayer;
    private float horizontalInput;
    private bool facingRight = true;
    private float currentSpeed;
    private float speedBoostTimer;
    private bool isSpeedBoosted;
    [SerializeField] private GameObject idleSprite;
    [SerializeField] private GameObject idleLeftSprite;
    [SerializeField] private GameObject walkSprite;
    [SerializeField] private GameObject walkLeftSprite;
    private bool isGrounded;
    private bool canJump;
    private bool isGroundPounding;

    void Start()
    {
        transform.SetParent(null); // Ensure player isn't parented to anything

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D missing on Player!");
            return;
        }

        currentSpeed = moveSpeed;
        idleSprite.SetActive(true);
        idleLeftSprite.SetActive(false);
        walkSprite.SetActive(false);
        walkLeftSprite.SetActive(false);

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck Transform not assigned on Player!");
        }

        canJump = true;
    }

    void Update()
    {
        if (isSpeedBoosted)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                currentSpeed = moveSpeed;
                isSpeedBoosted = false;
            }
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0)
        {
            facingRight = horizontalInput > 0;
        }

        if (horizontalInput > 0)
        {
            SetSpriteActive(walkSprite);
        }
        else if (horizontalInput < 0)
        {
            SetSpriteActive(walkLeftSprite);
        }
        else
        {
            SetSpriteActive(facingRight ? idleSprite : idleLeftSprite);
        }

        // Check if player is grounded using raycast
        isGrounded = false;
        if (groundCheck != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
            if (hit.collider != null && hit.collider.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        if (isGrounded && !canJump)
        {
            canJump = true;
            isGroundPounding = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded && canJump)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canJump = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && !isGrounded && !isGroundPounding)
        {
            isGroundPounding = true;
            rb.linearVelocity = new Vector2(0, -groundPoundForce);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * currentSpeed, rb.linearVelocity.y);
    }

    private void SetSpriteActive(GameObject activeSprite)
    {
        idleSprite.SetActive(activeSprite == idleSprite);
        idleLeftSprite.SetActive(activeSprite == idleLeftSprite);
        walkSprite.SetActive(activeSprite == walkSprite);
        walkLeftSprite.SetActive(activeSprite == walkLeftSprite);
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        currentSpeed = moveSpeed * multiplier;
        speedBoostTimer = duration;
        isSpeedBoosted = true;
    }
}
