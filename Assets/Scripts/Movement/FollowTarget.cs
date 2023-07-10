using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Movement/Follow Target")]
[RequireComponent(typeof(Rigidbody2D))]
public class FollowTarget : Physics2DObject
{
    // This is the target the object is going to move towards
    public Transform target;

    [Header("Movement")]
    // Speed used to move towards the target
    public float speed = 1f;

    // Used to decide if the object will look at the target while pursuing it
    public bool lookAtTarget = false;

    // The direction that will face the target
    public Enums.Directions useSide = Enums.Directions.Up;

    [Header("Following Distance")]
    // The distance at which the object will start following the target
    public float followDistance = 4f;

    // Delay duration in seconds after hurting the player
    public float hurtDelay = 2f;

    private bool isHurtCooldown = false;

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        // Do nothing if the target hasn't been assigned or it was destroyed for some reason
        if (target == null)
            return;

        // Check if the enemy is in hurt cooldown
        if (isHurtCooldown)
            return;

        // Check if the distance to the target is within the follow distance
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance <= followDistance)
        {
            // Look towards the target
            if (lookAtTarget)
            {
                Utils.SetAxisTowards(useSide, transform, target.position - transform.position);
            }

            // Move towards the target
            rigidbody2D.MovePosition(Vector2.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed));
        }
    }

    // Call this method when the enemy hurts the player
    public void StartHurtCooldown()
    {
        // Start the hurt cooldown delay
        StartCoroutine(HurtCooldownDelay());
    }

    private IEnumerator HurtCooldownDelay()
    {
        // Set the hurt cooldown flag to true
        isHurtCooldown = true;

        // Wait for the specified duration
        yield return new WaitForSeconds(hurtDelay);

        // Reset the hurt cooldown flag
        isHurtCooldown = false;
    }
}