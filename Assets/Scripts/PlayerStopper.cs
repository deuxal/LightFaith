using UnityEngine;

public class PlayerStopper : MonoBehaviour
{
    public float movementSpeed = 5f;

    private bool isPlayerStopped = false;

    private void Update()
    {
        if (isPlayerStopped)
            return;

        // Player movement logic here
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0f) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    public void StopPlayer()
    {
        isPlayerStopped = true;
    }

    public void ResumePlayer()
    {
        isPlayerStopped = false;
    }
}
