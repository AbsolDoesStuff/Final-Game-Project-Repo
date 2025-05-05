using UnityEngine;

public class SmallEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private GameObject smallEnemyFacingLeft;
    [SerializeField] private GameObject smallEnemyFacingRight;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D missing on SmallEnemy: " + gameObject.name);
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

        // Set the default sprite direction
        smallEnemyFacingLeft.SetActive(false);
        smallEnemyFacingRight.SetActive(true);
    }

    void Update()
    {
        // No projectiles, so just check health and destroy if needed
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null && enemyHealth.health <= 0)
        {
            DestroyEnemy();
        }
    }

    void FixedUpdate()
    {
        if (player != null && rb != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

            // Handle sprite flipping based on direction
            bool facingRight = direction.x >= 0;
            smallEnemyFacingLeft.SetActive(!facingRight);
            smallEnemyFacingRight.SetActive(facingRight);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Ensuring small enemies take 50 damage and are destroyed
                enemyHealth.TakeDamage(50f);  // This will destroy the small enemy after one hit
            }
        }
    }

    private void DestroyEnemy()
    {
        // Destroy the small enemy object once health is 0 or below
        Debug.Log(gameObject.name + " has been destroyed.");
        Destroy(gameObject);  // Destroy this SmallEnemy object
    }
}
