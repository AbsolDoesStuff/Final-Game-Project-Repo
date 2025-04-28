using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    private float horizontalInput;
    private bool facingRight = true;
    private float currentSpeed;
    private float speedBoostTimer;
    private bool isSpeedBoosted;
    [SerializeField] private GameObject idleSprite;
    [SerializeField] private GameObject idleLeftSprite;
    [SerializeField] private GameObject walkSprite;
    [SerializeField] private GameObject walkLeftSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
        idleSprite.SetActive(true);
        idleLeftSprite.SetActive(false);
        walkSprite.SetActive(false);
        walkLeftSprite.SetActive(false);
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