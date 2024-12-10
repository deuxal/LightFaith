using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Image staminaBarImage;
    public ObjectMovement objectMovement;
    public float fillSpeed = 2f;

    private float maxStamina;
    private float currentStamina;

    private void Start()
    {
        maxStamina = objectMovement.SprintDuration; // Usar SprintDuration
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (objectMovement.IsSprinting) // Usar IsSprinting
        {
            currentStamina -= Time.deltaTime * fillSpeed;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += Time.deltaTime * fillSpeed;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        float fillAmount = currentStamina / maxStamina;
        staminaBarImage.fillAmount = Mathf.Lerp(staminaBarImage.fillAmount, fillAmount, Time.deltaTime * fillSpeed);
    }
}
