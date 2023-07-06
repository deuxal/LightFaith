using UnityEngine;
using System;

[AddComponentMenu("Playground/Attributes/Health System")]
public class HealthSystemAttribute : MonoBehaviour
{
    public int health = 3;
    private int originalHealth;
    private UIScript ui;
    private int maxHealth;
    private int playerNumber;

    public event Action<int> OnHealthModified; // Event to notify when health is modified

    private void Start()
    {
        ui = GameObject.FindObjectOfType<UIScript>();

        switch (gameObject.tag)
        {
            case "Player":
                playerNumber = 0;
                break;
            case "Player2":
                playerNumber = 1;
                break;
            default:
                playerNumber = -1;
                break;
        }

        if (ui != null && playerNumber != -1)
        {
            ui.SetHealth(health, playerNumber);
        }

        maxHealth = health;
        originalHealth = health;
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public void ModifyHealth(int amount)
    {
        if (health + amount > maxHealth)
        {
            amount = maxHealth - health;
        }

        health += amount;

        if (ui != null && playerNumber != -1)
        {
            ui.ChangeHealth(amount, playerNumber);
        }

        OnHealthModified?.Invoke(health); // Invoke the OnHealthModified event with the updated health value

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void ResetHealth()
    {
        health = originalHealth;
    }
}