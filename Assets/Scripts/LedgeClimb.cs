using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeClimb : MonoBehaviour
{
    public float climbSpeed = 5f;
    public float edgeRaycastDistance = 1f;
    public LayerMask edgeLayer;
    public float edgeClimbOffset = 1f;

    private bool isClimbing = false;
    private Vector3 climbDestination;

    private void Update()
    {
        // Check if the player is currently climbing
        if (isClimbing)
        {
            // Move towards the climb destination
            float step = climbSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, climbDestination, step);

            // Check if the player has reached the climb destination
            if (transform.position == climbDestination)
            {
                isClimbing = false;
            }

            return;
        }

        // Check for edge climb input (e.g., pressing the "CTRL" key)
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            // Perform a raycast forward and downward to detect edges
            RaycastHit2D hitForward = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, edgeRaycastDistance, edgeLayer);
            RaycastHit2D hitDownward = Physics2D.Raycast(transform.position, Vector2.down, edgeRaycastDistance, edgeLayer);

            // If both raycasts hit edges, start climbing
            if (hitForward.collider != null && hitDownward.collider != null)
            {
                isClimbing = true;

                // Calculate the climb destination based on the hit points of the raycasts
                float climbX = hitForward.point.x;
                float climbY = hitDownward.point.y + edgeClimbOffset;
                climbDestination = new Vector3(climbX, climbY, transform.position.z);
            }
        }
    }
}

