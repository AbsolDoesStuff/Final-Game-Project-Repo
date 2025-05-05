using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool isLargeEnemy = false; // Public for accessing from other scripts
    public float health = 100f; // Default health value
    public float largeEnemyHealth = 200f; // Health for large enemies
    public float smallEnemyHealth = 100f; // Health for small enemies
    public float damageTaken = 0f; // Total damage taken

    private void Start()
    {
        // Initialize health based on whether it's a large or small enemy
        if (isLargeEnemy)
        {
            health = largeEnemyHealth; // Set to large enemy health
        }
        else
        {
            health = smallEnemyHealth; // Set to small enemy health
        }
    }

    public void TakeDamage(float damage)
    {
        // Reduce health by damage taken
        health -= damage;
        damageTaken += damage; // Track total damage taken

        // Print to debug when health is modified
        Debug.Log("Health: " + health);

        // Check if health is zero or below, if so destroy the enemy
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Destroy the enemy game object when health reaches zero or below
        Debug.Log(gameObject.name + " has been destroyed.");
        Destroy(gameObject); // Or add animation/death effects here
    }

    public void Heal(float healingAmount)
    {
        // Add health up to the max health for the specific enemy
        health += healingAmount;
        if (health > (isLargeEnemy ? largeEnemyHealth : smallEnemyHealth)) 
        {
            health = isLargeEnemy ? largeEnemyHealth : smallEnemyHealth; // Cap at max health
        }
    }

    // Add this method to fix the SmallEnemy script error
    public float GetCurrentHealth()
    {
        return health;
    }
}
