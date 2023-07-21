using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepSound : MonoBehaviour
{
    public float stepInterval = 0.5f; // Adjust this value to control the step sound frequency
    public float walkThreshold = 0.1f; // Adjust this value to control the walking threshold

    private AudioSource audioSource;
    private bool isWalking = false;
    private float stepTimer = 0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if the character is walking
        isWalking = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > walkThreshold;

        // Play step sound at regular intervals when walking
        if (isWalking)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                stepTimer = 0f;
                PlayStepSound();
            }
        }
    }

    private void PlayStepSound()
    {
        // Check if there's a valid AudioSource component and it has an assigned AudioClip
        if (audioSource != null && audioSource.clip != null)
        {
            // Play the step sound through the AudioSource
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}

