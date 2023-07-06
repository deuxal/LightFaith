using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image[] healthBars; // An array of health bars in the HealthUI image
    public HealthSystemAttribute healthSystem; // Reference to the HealthSystemAttribute script or component

    private int maxHealthBars;

    private void Start()
    {
        maxHealthBars = healthBars.Length;
        UpdateHealthBars();
        healthSystem.OnHealthModified += OnHealthModified;
    }

    private void OnHealthModified(int currentHealth)
    {
        UpdateHealthBars();
    }

    private void UpdateHealthBars()
    {
        int currentHealth = healthSystem.GetCurrentHealth();

        // Calculate the number of active health bars based on the current health
        int activeHealthBars = Mathf.CeilToInt((float)currentHealth / maxHealthBars);

        // Activate/deactivate bars based on the activeHealthBars count
        for (int i = 0; i < healthBars.Length; i++)
        {
            healthBars[i].gameObject.SetActive(i < activeHealthBars);
        }
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthModified -= OnHealthModified;
    }
}

