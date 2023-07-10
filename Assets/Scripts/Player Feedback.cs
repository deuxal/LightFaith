using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public bool isHurt = false; // Indicates if the player is currently hurt
    public float hitForce = 5f; // The backward force applied to the player when hit
    public float hitDuration = 0.2f; // The duration in seconds for the hit effect
    public float hitCooldown = 1f; // The cooldown period in seconds before the player can get hurt again

    private Rigidbody2D rb;
    private bool isCooldownActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isHurt && !isCooldownActive)
        {
            // Apply hit effect
            animator.SetBool("IsHurt", true); // Set the "IsHurt" parameter to true in the Animator
            rb.AddForce(Vector2.left * hitForce, ForceMode2D.Impulse);
            isHurt = true;

            // Reset hit effect after a duration
            Invoke(nameof(ResetHitEffect), hitDuration);

            // Start cooldown period
            isCooldownActive = true;
            Invoke(nameof(ResetCooldown), hitCooldown);

            // Log hit event
            Debug.Log("Player hit.");
        }
    }

    private void ResetHitEffect()
    {
        animator.SetBool("IsHurt", false); // Set the "IsHurt" parameter to false in the Animator
        isHurt = false;
    }

    private void ResetCooldown()
    {
        isCooldownActive = false;
    }
}
