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
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Vector3 direction = (mouseWorldPosition - transform.position).normalized;

        float aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Upper Left
        if (angle >= -135f && angle < -50f)
        {
            animator.SetFloat("AimDirection", 1f); // Set the "AimDirection" parameter to 1 for Upper Left
        }
        // Left
        else if (angle >= -180f && angle < -135f)
        {
            animator.SetFloat("AimDirection", 2f); // Set the "AimDirection" parameter to 2 for Left
        }
        // Bottom Left
        else if (angle >= 135f || angle < -135f)
        {
            animator.SetFloat("AimDirection", 3f); // Set the "AimDirection" parameter to 3 for Bottom Left
        }
        // Bottom Right
        else if (angle >= -135f && angle < -45f)
        {
            animator.SetFloat("AimDirection", 4f); // Set the "AimDirection" parameter to 4 for Bottom Right
        }
        // Right
        else if (angle >= -45f && angle < 45f)
        {
            animator.SetFloat("AimDirection", 5f); // Set the "AimDirection" parameter to 5 for Right
        }
        // Upper Right
        else if (angle >= 50f && angle < 135f)
        {
            animator.SetFloat("AimDirection", 6f); // Set the "AimDirection" parameter to 6 for Upper Right
        }
        // Up (Aiming Up)
        else if (angle >= 135f && angle < 225f)
        {
            animator.SetFloat("AimDirection", 7f); // Set the "AimDirection" parameter to 7 for Aiming Up
        }
        // Down
        else if (angle >= -135f && angle < -225f)
        {
            animator.SetFloat("AimDirection", 8f); // Set the "AimDirection" parameter to 8 for Down
        }
        // Default (No aim direction)
        else
        {
            animator.SetFloat("AimDirection", 0f); // Set the "AimDirection" parameter to 0 for default (No aim direction)
        }

    }
}