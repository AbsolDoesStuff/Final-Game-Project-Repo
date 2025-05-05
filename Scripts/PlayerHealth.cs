using UnityEngine; // Make sure this is at the top

public class PlayerHealth : MonoBehaviour
{
    private float health = 250f;
    public float CurrentHealth => health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Debug.Log("Player died!");
            // Add death logic later (e.g., game over)
        }
    }
}
