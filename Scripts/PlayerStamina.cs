using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    private float currentStamina;
    private float staminaDepletionRate = 1f;
    [SerializeField] private Slider staminaSlider;

    private float speedBoostTimer;
    private bool isSpeedBoosted;

    private void Start()
    {
        currentStamina = maxStamina;
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }
    }

    private void Update()
    {
        if (isSpeedBoosted)
        {
            staminaDepletionRate = 2f; // Increase the depletion rate while speed boosted
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                isSpeedBoosted = false;
                staminaDepletionRate = 1f; // Reset to normal rate after boost ends
            }
        }

        if (currentStamina > 0)
        {
            currentStamina -= staminaDepletionRate * Time.deltaTime;
        }
        else
        {
            currentStamina = 0;
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }

    public void RefillStamina()
    {
        currentStamina = maxStamina;
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        speedBoostTimer = duration;
        isSpeedBoosted = true;
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }
}
