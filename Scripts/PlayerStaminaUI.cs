using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaUI : MonoBehaviour
{
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private float drainDuration = 10f;

    private float timer = 0f;
    private bool isDraining = false;

    void Start()
    {
        if (staminaSlider != null)
        {
            staminaSlider.minValue = 0;
            staminaSlider.maxValue = 1;
            staminaSlider.value = 0; // Start empty
        }
    }

    void Update()
    {
        if (isDraining && timer > 0)
        {
            timer -= Time.deltaTime;
            staminaSlider.value = timer / drainDuration;

            if (timer <= 0)
            {
                isDraining = false;
                staminaSlider.value = 0;
            }
        }
    }

    public void FillStamina()
    {
        timer = drainDuration;
        staminaSlider.value = 1f;
        isDraining = true;
    }
}
