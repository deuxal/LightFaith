using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    public Sprite[] sprites; // Array of sprite images for different directions
    public Transform crosshair; // Reference to the crosshair object

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (crosshair != null)
        {
            Vector3 direction = crosshair.position - transform.position;
            direction.z = 0f;
            if (direction.magnitude > 0.01f)
            {
                // Calculate the angle between the direction and the up vector
                float angle = Vector3.SignedAngle(Vector3.up, direction.normalized, Vector3.forward);

                // Determine the index of the sprite image based on the angle
                int spriteIndex = GetSpriteIndex(angle);

                // Set the sprite image
                spriteRenderer.sprite = sprites[spriteIndex];
            }
        }
    }

    private int GetSpriteIndex(float angle)
    {
        // Calculate the index of the sprite image based on the angle range
        // Adjust the angles and sprite indices according to your specific sprite setup
        if (angle >= -45f && angle < 45f)
        {
            return 0; // Sprite index for right direction
        }
        else if (angle >= 45f && angle < 135f)
        {
            return 1; // Sprite index for up direction
        }
        else if (angle >= -135f && angle < -45f)
        {
            return 3; // Sprite index for down direction
        }
        else
        {
            return 2; // Sprite index for left direction
        }
    }
}

