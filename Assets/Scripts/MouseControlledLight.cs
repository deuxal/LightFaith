using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(AudioSource))] // Ensure that an AudioSource component is attached to the GameObject
public class MouseControlledLight : MonoBehaviour
{
    public Light2D globalLight2D;
    public KeyCode toggleKey = KeyCode.E;
    public KeyCode modeChangeKey = KeyCode.Space;
    public float batteryDuration = 10f; // Duration of the battery in seconds

    public AudioClip flashlightSoundClip; // Audio clip for flashlight activation

    private bool isLightOn = true;
    private bool isProjectileMode = false;
    private float batteryTimer;
    private float originalBatteryDuration;

    private AudioSource audioSource; // Reference to the AudioSource component
    private ObjectMovement playerMovementController; // Reference to the ObjectMovement script

    private void Start()
    {
        playerMovementController = FindObjectOfType<ObjectMovement>(); // Find the ObjectMovement script in the scene

        // Add an AudioSource component if not already present
        if (!TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Deactivate the flashlight at the start of the game
        globalLight2D.enabled = false;
        isLightOn = false;

        originalBatteryDuration = batteryDuration; // Store the original battery duration
        batteryTimer = batteryDuration; // Set the initial battery timer
    }

    private void Update()
    {
        // Toggle light on/off when the specified key is pressed
        if (Input.GetKeyDown(toggleKey))
        {
            isLightOn = !isLightOn;
            globalLight2D.enabled = isLightOn;

            // Play the flashlight activation sound when the flashlight is turned on
            if (isLightOn && flashlightSoundClip != null)
            {
                audioSource.PlayOneShot(flashlightSoundClip);
            }
        }

        // Change mode when the specified key is held down
        if (Input.GetKey(modeChangeKey))
        {
            isProjectileMode = true;
            globalLight2D.enabled = false;
            playerMovementController.StopPlayer(); // Stop the player's movement
        }
        else
        {
            isProjectileMode = false;
            globalLight2D.enabled = isLightOn;
            playerMovementController.ResumePlayer(); // Resume the player's movement
        }

        // Decrease the battery timer if the light is on and not in ProjectileMode
        if (isLightOn && !isProjectileMode)
        {
            batteryTimer -= Time.deltaTime;

            if (batteryTimer <= 0f)
            {
                batteryTimer = 0f;
                isLightOn = false;
                globalLight2D.enabled = false;
            }
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

    public void ResetBatteryDuration()
    {
        batteryDuration = originalBatteryDuration;
        batteryTimer = batteryDuration;
    }

    public float BatteryPercentage()
    {
        return batteryTimer / batteryDuration;
    }
}



