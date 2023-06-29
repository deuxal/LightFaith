using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteController : MonoBehaviour
{
    public Sprite[] sprites; // Array of sprite images for different directions

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Get the cursor position in screen coordinates
        Vector3 cursorPosition = Input.mousePosition;

        // Convert the cursor position to world coordinates
        Vector3 worldCursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);
        worldCursorPosition.z = 0f;

        // Calculate the direction from the player to the cursor position
        Vector3 direction = worldCursorPosition - transform.position;

        // Calculate the angle between the direction and the up vector
        float angle = Vector3.SignedAngle(Vector3.up, direction.normalized, Vector3.forward);

        // Determine the index of the sprite image based on the angle
        int spriteIndex = GetSpriteIndex(angle);

        // Set the sprite image
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private int GetSpriteIndex(float angle)
    {
        // Calculate the index of the sprite image based on the angle range
        // Adjust the angles and sprite indices according to your specific sprite setup

        // Angles for each direction
        float bottomLeftAngle = -135f;
        float leftAngle = -90f;
        float upperLeftAngle = -45f;
        float upAngle = 0f;
        float rightAngle = 90f;
        float upperRightAngle = 45f;
        float bottomRightAngle = 135f;

        // Determine the index based on angle comparison
        if (angle >= bottomLeftAngle && angle < leftAngle)
        {
            return 0; // Sprite index for bottom left direction
        }
        else if (angle >= leftAngle && angle < upperLeftAngle)
        {
            return 1; // Sprite index for left direction
        }
        else if (angle >= upperLeftAngle && angle < upAngle)
        {
            return 2; // Sprite index for upper left direction
        }
        else if (angle >= upAngle && angle < rightAngle)
        {
            return 3; // Sprite index for up direction
        }
        else if (angle >= rightAngle && angle < upperRightAngle)
        {
            return 4; // Sprite index for right direction
        }
        else if (angle >= upperRightAngle && angle < bottomRightAngle)
        {
            return 5; // Sprite index for upper right direction
        }
        else
        {
            return 6; // Sprite index for bottom right direction
        }
    }
}
