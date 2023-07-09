using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoutherAI : MonoBehaviour
{
    public Transform player;
    public float jumpForce = 5f;
    public float stopDistance = 5f;
    public float shootingCooldown = 2f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileForce = 10f;
    public float projectileSelfDestructTime = 5f; // Time until the projectile self-destructs

    private bool isJumping = false;
    private bool isShooting = false;
    private bool isCooldownActive = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Jumping behavior
            if (!isJumping && distanceToPlayer < stopDistance)
            {
                Jump();
            }

            // Shooting behavior
            if (!isShooting && distanceToPlayer < stopDistance && !isCooldownActive)
            {
                Shoot();
            }
        }
    }

    private void Jump()
    {
        // Perform jump action
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        isJumping = true;

        // Reset jumping state after a delay
        Invoke(nameof(ResetJumping), 1f);
    }

    private void ResetJumping()
    {
        isJumping = false;
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
