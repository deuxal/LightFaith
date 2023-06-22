using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Image staminaBarImage;
    public ObjectMovement objectMovement;
    public float fillSpeed = 2f; // Adjust the speed at which the stamina bar drains and refills

    private float maxStamina;
    private float currentStamina;

    private void Start()
    {
        maxStamina = objectMovement.sprintDuration;
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (objectMovement.isSprinting)
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

        // Smoothly update the fill amount using Lerp
        float smoothedFillAmount = Mathf.Lerp(staminaBarImage.fillAmount, fillAmount, Time.deltaTime * fillSpeed);
        staminaBarImage.fillAmount = smoothedFillAmount;
    }
}
