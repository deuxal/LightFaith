using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public Transform pointA;
    public Transform pointB;

    private Transform player;
    private bool isPlayerDetected = false;
    private bool isMovingToPointA = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isPlayerDetected)
        {
            ChasePlayer();
        }
        else
        {
            MoveBetweenPoints();
        }
    }

    private void MoveBetweenPoints()
    {
        Vector3 targetPosition = isMovingToPointA ? pointA.position : pointB.position;
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Rotate the sprite based on the movement direction
        float rotationY = direction.x > 0f ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        // Check if the enemy has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMovingToPointA = !isMovingToPointA;
        }

        // Check if the player is within the detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            isPlayerDetected = true;
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        // Rotate the sprite based on the movement direction
        float rotationY = direction.x > 0f ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);

        // Check if the player is no longer within the detection range
        if (Vector3.Distance(transform.position, player.position) > detectionRange)
        {
            isPlayerDetected = false;
        }
    }
}



