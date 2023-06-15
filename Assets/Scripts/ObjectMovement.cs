using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public Collider2D myCollider;
    public LedgeClimb lc;
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

        if (isPlayerStopped) {
            currentSpeed = 0;
            return;
        }

        if(lc != null) { 
            lc.MoveToPoint(this.transform, () => {TurnOff();}, () => { TurnOn(); });
        }

    }
    private void FixedUpdate()
    {
        // Movimiento horizontal
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        transform.position += (Vector3.right * horizontalMovement * currentSpeed * Time.fixedDeltaTime);
        // transform.Translate
    }

    public void TurnOff() 
    {
        myCollider.enabled = false;
    }

    public void TurnOn()
    {
        myCollider.enabled = true;
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
