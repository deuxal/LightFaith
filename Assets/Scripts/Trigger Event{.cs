using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEvent : MonoBehaviour
{
    public GameObject rainPrefab;
    public GameObject spriteToAppear;
    public AudioSource soundSource;
    public AudioClip appearSoundClip;
    public Animator redFadeAnimator;
    public string creditsSceneName;

    private bool eventTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !eventTriggered)
        {
            eventTriggered = true;

            // Play the sound clip when the player enters the trigger area
            if (soundSource != null && appearSoundClip != null)
            {
                soundSource.PlayOneShot(appearSoundClip);
            }

            // Activate the red fade effect
            if (redFadeAnimator != null)
            {
                redFadeAnimator.SetTrigger("RedFadeIn");
            }

            // Disable the rain prefab
            if (rainPrefab != null)
            {
                rainPrefab.SetActive(false);
            }

            // Show the sprite (assuming it was initially inactive)
            if (spriteToAppear != null)
            {
                spriteToAppear.SetActive(true);
            }

            // Load the credits scene
            if (!string.IsNullOrEmpty(creditsSceneName))
            {
                SceneManager.LoadScene(creditsSceneName);
            }
        }
    }
}
