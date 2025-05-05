using UnityEngine;

public class LargeEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private GameObject largeEnemyFacingLeft_0;
    [SerializeField] private GameObject largeEnemyFacingRight_0;

    private Transform player;
    private Rigidbody2D rb;
    private float fireTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D missing on LargeEnemy: " + gameObject.name);
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

        largeEnemyFacingLeft_0.SetActive(false);
        largeEnemyFacingRight_0.SetActive(true);

        fireTimer = fireRate;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0 && player != null)
        {
            ShootAtPlayer();
            fireTimer = fireRate;
        }
    }

    void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

            bool facingRight = direction.x >= 0;
            largeEnemyFacingLeft_0.SetActive(!facingRight);
            largeEnemyFacingRight_0.SetActive(facingRight);
        }
    }

    void ShootAtPlayer()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile Prefab not assigned on LargeEnemy!");
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 spawnPosition = (Vector2)transform.position + direction * 0.5f;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
        {
            projScript.SetDirection(direction);
        }
        else
        {
            Debug.LogError("Projectile script missing on projectilePrefab!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Ensure damage is passed as an integer
                enemyHealth.TakeDamage((int)50f);  // Casting float to int
            }
        }
    }
}
