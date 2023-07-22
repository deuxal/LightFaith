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
    private AudioSource deathAudioSource;


    private void Start()
    {
        originalHealth = health;

        // Find or create the AudioHolder GameObject and attach the AudioSource
        GameObject audioHolderObj = GameObject.Find("AudioHolder");
        if (audioHolderObj == null)
        {
            audioHolderObj = new GameObject("AudioHolder");
            deathAudioSource = audioHolderObj.AddComponent<AudioSource>();
        }
        else
        {
            deathAudioSource = audioHolderObj.GetComponent<AudioSource>();
            if (deathAudioSource == null)
            {
                deathAudioSource = audioHolderObj.AddComponent<AudioSource>();
            }
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
        if (deathAudioSource != null && hitSoundClip != null) // Change 'audioSource' to 'deathAudioSource'
        {
            deathAudioSource.PlayOneShot(hitSoundClip); // Change 'audioSource' to 'deathAudioSource'
        }
    }

    public void PlayDeathSound()
    {
        if (deathAudioSource != null && deathSoundClip != null)
        {
            deathAudioSource.PlayOneShot(deathSoundClip);
        }
    }
}
