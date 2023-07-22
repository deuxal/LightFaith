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
    public AudioClip hitSoundClip; // Audio clip for hit sound
    public AudioClip deathSoundClip; // Audio clip for death sound

    private AudioSource audioSource;

    private void Start()
    {
        originalHealth = health;

        // Add an AudioSource component if not already present
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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
            PlayHitSound(); // Play the hit sound whenever the enemy gets hit
        }
    }

    public void ResetHealth()
    {
        health = originalHealth;
    }

    private void Die()
    {
        PlayDeathParticles();
        PlayDeathSound(); // Play the death sound when the enemy dies
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

    public void PlayHitSound()
    {
        if (audioSource != null && hitSoundClip != null)
        {
            audioSource.PlayOneShot(hitSoundClip);
        }
    }

    public void PlayDeathSound()
    {
        if (audioSource != null && deathSoundClip != null)
        {
            audioSource.PlayOneShot(deathSoundClip);
        }
    }
}
