using UnityEngine;
using System;

public class EnemyHealthSystem : MonoBehaviour
{
    public int health = 3;
    private int originalHealth;

    public event Action<int> OnHealthModified; // Event to notify when health is modified
    public bool deathOnZeroHp = true;

    public GameObject hitParticlesPrefab; // Prefab for hit particles
    public GameObject deathParticlesPrefab; // Prefab for death particles

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
        else
        {
            PlayHitParticles();
        }
    }

    public void ResetHealth()
    {
        health = originalHealth;
    }

    private void Die()
    {
        PlayDeathParticles();
        // Perform death actions here, such as playing death animation, disabling movement, etc.
        Destroy(gameObject);
    }

    private void PlayHitParticles()
    {
        if (hitParticlesPrefab != null)
        {
            GameObject particles = Instantiate(hitParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 2f); // Destroy particles after 2 seconds
        }
    }

    private void PlayDeathParticles()
    {
        if (deathParticlesPrefab != null)
        {
            GameObject particles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 2f); // Destroy particles after 2 seconds
        }
    }
}
