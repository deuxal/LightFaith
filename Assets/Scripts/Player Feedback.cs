using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    public Sprite Hurt; // The sprite to use when the player gets hurt
    public float hitForce = 5f; // The backward force applied to the player when hit
    public float hitDuration = 0.2f; // The duration in seconds for the hit effect
    public float hitCooldown = 1f; // The cooldown period in seconds before the player can get hit again

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Sprite originalSprite;
    private bool isHit = false;
    private bool isCooldownActive = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        originalSprite = spriteRenderer.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isHit && !isCooldownActive)
        {
            // Apply hit effect
            spriteRenderer.sprite = Hurt;
            rb.AddForce(Vector2.left * hitForce, ForceMode2D.Impulse);
            isHit = true;

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
        spriteRenderer.sprite = originalSprite;
        isHit = false;
    }

    private void ResetCooldown()
    {
        isCooldownActive = false;
    }
}

