using UnityEngine;

public class AnimationAudioController : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource component that will play the audio

    // The method to be called by the animation event to play the audio
    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}

