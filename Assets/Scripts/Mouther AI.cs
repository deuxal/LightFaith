using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoutherAI : MonoBehaviour
{
    public Transform player;
    public float jumpForce = 5f;
    public float forwardForce = 2f;
    public float stopDistance = 5f;
    public float shootingCooldown = 2f;
    public float jumpCooldown = 2f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileForce = 10f;
    public float projectileSelfDestructTime = 5f; // Time until the projectile self-destructs

    private bool isJumping = false;
    private bool isShooting = false;
    private bool isCooldownActive = false;
    private bool isJumpCooldownActive = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player != null)
        {
            // Shooting behavior
            if (!isShooting)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (distanceToPlayer < stopDistance && !isCooldownActive)
                {
                    Shoot();
                    return; // Skip jumping behavior if shooting is triggered
                }
            }

            // Jumping behavior
            if (!isJumping && !isJumpCooldownActive)
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        // Perform jump action
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        rb.AddForce(new Vector2(forwardForce, 0f), ForceMode2D.Impulse);
        isJumping = true;
        isJumpCooldownActive = true;

        // Reset jumping state and activate jump cooldown after a delay
        Invoke(nameof(ResetJumping), 1f);
        Invoke(nameof(ResetJumpCooldown), jumpCooldown);
    }

    private void ResetJumping()
    {
        isJumping = false;
    }

    private void ResetJumpCooldown()
    {
        isJumpCooldownActive = false;
    }

    private void Shoot()
    {
        // Perform shoot action
        Vector3 direction = (player.position - projectileSpawnPoint.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);

        // Self-destruct timer for the projectile
        Destroy(projectile, projectileSelfDestructTime);

        // Start cooldown
        isShooting = true;
        isCooldownActive = true;
        Invoke(nameof(ResetShooting), shootingCooldown);
        Invoke(nameof(ResetCooldown), shootingCooldown + 1f);
    }

    private void ResetShooting()
    {
        isShooting = false;
    }

    private void ResetCooldown()
    {
        isCooldownActive = false;
    }
}
