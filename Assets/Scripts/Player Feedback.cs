using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerEnd : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    public GameObject rainPrefab; // Reference to the rain prefab GameObject
    public Animator redFadeAnimator; // Reference to the red fade animator
    public AudioClip rapidSpriteSoundClip; // Sound clip for rapid sprite event
    public AudioClip redFadeSoundClip; // Sound clip for red fade event
    public string creditsSceneName = "CreditsScene"; // Name of the credits scene to load

    private AudioSource audioSource;
    private bool triggered = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has entered the trigger area and the events haven't been triggered yet
        if (other.gameObject == player && !triggered)
        {
            triggered = true;

            // Disable the rain prefab
            rainPrefab.SetActive(false);

            // Play rapid sprite sound clip
            if (rapidSpriteSoundClip != null)
            {
                audioSource.PlayOneShot(rapidSpriteSoundClip);
            }

            // Start the coroutine to handle the animations and scene loading
            StartCoroutine(TriggerSequence());
        }
    }

    private IEnumerator TriggerSequence()
    {
        // Wait for the sprite animation to finish
        Animation spriteAnimation = player.GetComponent<Animation>();
        if (spriteAnimation != null)
        {
            yield return new WaitForSeconds(spriteAnimation.clip.length);
        }
        else
        {
            Debug.LogWarning("Sprite animation component not found on player.");
        }

        // Trigger the red fade animation
        redFadeAnimator.SetTrigger("FadeIn");

        // Play red fade sound clip
        if (redFadeSoundClip != null)
        {
            audioSource.PlayOneShot(redFadeSoundClip);
        }

        // Wait for the red fade animation to finish
        yield return new WaitForSeconds(redFadeAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Load the credits scene
        SceneManager.LoadScene(creditsSceneName);
    }
}