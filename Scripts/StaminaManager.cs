using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    private float stamina = 100f;
    [SerializeField] private Slider staminaSlider;

    public void DepleteStamina(float amount)
    {
        stamina -= amount;
        staminaSlider.value = stamina / 100f; // Update stamina slider
    }

    public void RefillStamina()
    {
        stamina = 100f;
        staminaSlider.value = stamina / 100f; // Update stamina slider
    }

    public float GetStamina() => stamina;
}
