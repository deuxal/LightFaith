using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundManager : MonoBehaviour
{
    // Step sound variables
    public AudioSource stepSoundSource1;
    public AudioSource stepSoundSource2;
    private bool isWalking = false;
    private float stepSoundSpeedMultiplier = 1f; // Adjust this value to control the step sound speed

    private void Start()
    {
        // Get the AudioSource components
        stepSoundSource1 = gameObject.AddComponent<AudioSource>();
        stepSoundSource2 = gameObject.AddComponent<AudioSource>();

        // Set loop and volume properties for step sounds
        stepSoundSource1.loop = true;
        stepSoundSource1.volume = 0.5f;
        stepSoundSource2.loop = true;
        stepSoundSource2.volume = 0.5f;
    }

    public void PlayStepSounds(float currentSpeed)
    {
        // Update step sound pitch based on player's speed
        float pitchMultiplier = currentSpeed * stepSoundSpeedMultiplier;
        stepSoundSource1.pitch = pitchMultiplier;
        stepSoundSource2.pitch = pitchMultiplier;

        // Play or stop step sounds based on the walking state
        if (!isWalking)
        {
            isWalking = true;
            stepSoundSource1.Play();
            stepSoundSource2.Play();
        }
    }

    public void StopStepSounds()
    {
        isWalking = false;
        stepSoundSource1.Stop();
        stepSoundSource2.Stop();
    }
}
