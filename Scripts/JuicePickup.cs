using UnityEngine;

public class JuicePickup : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float speedBoostDuration = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            PlayerStaminaUI staminaUI = FindObjectOfType<PlayerStaminaUI>();

            if (playerMovement != null)
            {
                playerMovement.ApplySpeedBoost(speedMultiplier, speedBoostDuration);
            }

            if (staminaUI != null)
            {
                staminaUI.FillStamina();
            }

            Destroy(gameObject);
        }
    }
}
