using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MenuControlledLight : MonoBehaviour
{
    public Light2D menuLight2D;

    private void Update()
    {
        // Get the mouse position in screen space
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Convert the mouse position to world space
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; // Set the Z coordinate to ensure it's at the same depth as the light

        // Update the position of the menu light 2D to follow the mouse cursor
        menuLight2D.transform.position = mouseWorldPosition;
    }
}
