using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate = 2f;
    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float fireTimer;

    private bool isLargeEnemy = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D missing on Enemy: " + gameObject.name);
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer missing on Enemy: " + gameObject.name);
            return;
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player with tag 'Player' not found!");
        }

        fireTimer = fireRate;

        // Check if we can access the isLargeEnemy flag from the EnemyHealth script
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            isLargeEnemy = enemyHealth.isLargeEnemy;
        }
        else
        {
            Debug.LogWarning("EnemyHealth component not found on this enemy.");
        }
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0 && player != null)
        {
            if (isLargeEnemy) // Only shoot if large enemy
            {
                ShootAtPlayer();
            }
            fireTimer = fireRate;
        }
    }

    void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);
            spriteRenderer.flipX = direction.x > 0;
        }
    }

    void ShootAtPlayer()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab not assigned on Enemy!");
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Call the TakeDamage method with a float value
                enemyHealth.TakeDamage(50f);
            }
        }
    }
}
