using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float normalSpeed = 2f;
    public float sprintSpeed = 4f;

    private float currentSpeed;
    private bool isPlayerStopped = false;

    private void Start()
    {
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        // Cambiar velocidad al presionar Shift
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        if (isPlayerStopped)
            return;

        // Movimiento horizontal
        float horizontalMovement = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalMovement * currentSpeed * Time.deltaTime);
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
