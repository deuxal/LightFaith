using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileModeSpriteController : MonoBehaviour
{
    public Animator animator;
    public Projectile projectile;
    public Sprite[] sprites;
    private float aimDirection;

    private void Update()
    {
        bool isProjectileMode = Input.GetKey(KeyCode.Space); // Check if "SPACE" key is held down

        animator.SetBool("IsProjectileMode", isProjectileMode); // Set the "IsProjectileMode" parameter in the animator

        if (projectile != null && projectile.projectileModeCursor != null)
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            Vector3 direction = (mouseWorldPosition - transform.position).normalized;

            //Vector3 direction = projectile.projectileModeCursor.transform.position - transform.position;
            direction.z = 0f;
            if (direction.magnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                SetAnimatorParameters(angle);
            }
        }
        if (isProjectileMode)
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            Vector3 direction = (mouseWorldPosition - transform.position).normalized;

        }
        // Get the aim direction based on the cursor position
        aimDirection = GetAimDirection();

        // Update the aim direction parameter in the animator
        animator.SetFloat("AimDirection", aimDirection);
    }
    private float GetAimDirection()
    {
        // Calculate the aim direction based on the cursor position
        // You can implement your own logic here to determine the aim direction

        // Example: Calculate the angle between the player's position and the cursor position
        Vector3 playerToCursor = GetMouseWorldPosition() - transform.position;
        float angle = Vector3.SignedAngle(Vector3.up, playerToCursor.normalized, Vector3.forward);

        // Map the angle to the aim direction value range (-1 to 1)
        float aimDirection = Mathf.Clamp(angle / 180f, -1f, 1f);

        return aimDirection;
    }


    private static Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }


    private void SetAnimatorParameters(float angle)
    {
        // Calculate the index of the sprite image based on the angle range
        // Adjust the angles and sprite indices according to your specific sprite setup

        // Upper Left
        if (angle >= -135f && angle < -45f)
        {
            animator.SetFloat("Horizontal", -1f);
            animator.SetFloat("Vertical", 1f);
        }
        // Left
        else if (angle >= -180f && angle < -135f)
        {
            animator.SetFloat("Horizontal", -1f);
            animator.SetFloat("Vertical", 0f);
        }
        // Bottom Left
        else if (angle >= 135f || angle < -135f)
        {
            animator.SetFloat("Horizontal", -1f);
            animator.SetFloat("Vertical", -1f);
        }
        // Bottom Right
        else if (angle >= 45f && angle < 135f)
        {
            animator.SetFloat("Horizontal", 1f);
            animator.SetFloat("Vertical", -1f);
        }
        // Right
        else if (angle >= -45f && angle < 45f)
        {
            animator.SetFloat("Horizontal", 1f);
            animator.SetFloat("Vertical", 0f);
        }
        // Upper Right
        else if (angle >= -45f && angle < 45f)
        {
            animator.SetFloat("Horizontal", 1f);
            animator.SetFloat("Vertical", 1f);
        }
        // Up
        else if (angle >= 45f && angle < 135f)
        {
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 1f);
        }
        // Down
        else if (angle >= -135f && angle < -45f)
        {
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", -1f);
        }
    }
}