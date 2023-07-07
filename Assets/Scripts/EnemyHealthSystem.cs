using UnityEngine;
using System;

public class EnemyHealthSystem : MonoBehaviour
{
    public int health = 3;
    private int originalHealth;

    public event Action<int> OnHealthModified; // Event to notify when health is modified
    public bool deathOnZeroHp = true;

    private void Start()
    {
        originalHealth = health;
    }

    public int GetCurrentHealth()
    {
        return health;
    }

    public void ModifyHealth(int amount)
    {
        if (health + amount > originalHealth)
        {
            amount = originalHealth - health;
        }

        health += amount;

        OnHealthModified?.Invoke(health); // Invoke the OnHealthModified event with the updated health value

        if (health <= 0 && deathOnZeroHp)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        health = originalHealth;
    }

    private void Die()
    {
        // Perform death actions here, such as playing death animation, disabling movement, etc.
        Destroy(gameObject);
    }
}

