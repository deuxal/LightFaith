using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MouseControlledLight : MonoBehaviour
{
    public Light2D globalLight2D;
    public KeyCode toggleKey = KeyCode.E;
    public KeyCode modeChangeKey = KeyCode.Space;

    private bool isLightOn = true;
    private bool isProjectileMode = false;

    private void Update()
    {
        // Toggle light on/off when the specified key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            isLightOn = !isLightOn;
            globalLight2D.enabled = isLightOn;
        }

        // Change mode when the specified key is held down
        if (Input.GetKey(modeChangeKey))
        {
            isProjectileMode = true;
            globalLight2D.enabled = false;
        }
        else
        {
            isProjectileMode = false;
            globalLight2D.enabled = isLightOn;
        }

        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position to world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; // Set the Z coordinate to ensure it's at the same depth as the light

        // Update the position of the global light 2D or projectile cursor to follow the mouse cursor
        if (isProjectileMode)
        {
            // Handle projectile cursor logic here (e.g., updating the position of a crosshair sprite)
            // You can add your own projectile shooting logic using Input.GetMouseButtonDown(0) for left-click detection
        }
        else
        {
            // Update the position of the global light 2D to follow the mouse cursor
            if (isLightOn)
            {
                globalLight2D.transform.position = mouseWorldPosition;
            }
        }
    }
}


