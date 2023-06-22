using UnityEngine;
using UnityEngine.UI;

public class ObjectMovement : MonoBehaviour
{
    public Collider2D myCollider;
    public LedgeClimb lc;
    public float normalSpeed = 2f;
    public float sprintSpeed = 4f;
    public float sprintDuration = 5f;
    public float sprintCooldown = 2f;

    private float currentSpeed;
    private bool isPlayerStopped = false;
    private float sprintTimer;
    public bool isSprinting;
    private float sprintCooldownTimer;
    private bool isCooldown;
    private SpriteRenderer spriteRenderer;

    /// <summary>
    [SerializeField] private float sprintTime = 10;
    private float currentSprintTime = 0;
    [SerializeField] private float sprintRecoveryStop = 1;
    [SerializeField] private float sprintRecoveryWalk = 1;
    [SerializeField] private Image sprintSlider;

    /// </summary>
    private void Start()
    {
        currentSprintTime = sprintTime;
        currentSpeed = normalSpeed;
        sprintTimer = sprintDuration;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Slider();
    }

    private void Update()
    {
        if (!isCooldown && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && currentSprintTime > 0)
        {
            if (!isSprinting)
            {
                isSprinting = true;
                sprintTimer = sprintDuration;
            }

            currentSpeed = sprintSpeed;
            sprintTimer -= Time.deltaTime;
            currentSprintTime -= Time.deltaTime;

            if (sprintTimer <= 0f)
            {
                currentSpeed = normalSpeed;
                sprintCooldownTimer = sprintCooldown;
                isCooldown = true;
                isSprinting = false;
            }
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        if (isCooldown)
        {
            sprintCooldownTimer -= Time.deltaTime;
            if (sprintCooldownTimer <= 0f)
            {
                isCooldown = false;
                sprintCooldownTimer = sprintCooldown;
            }
        }

        if (isPlayerStopped)
        {
            currentSprintTime += Time.deltaTime * sprintRecoveryStop;
            currentSprintTime = currentSprintTime > sprintTime ? sprintTime : currentSprintTime;
            currentSpeed = 0;
            Slider();
            return;
        }

        if (lc != null)
        {
            lc.MoveToPoint(this.transform, () => { TurnOff(); }, () => { TurnOn(); });
        }

        // Rotate the sprite based on the input keys
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (horizontalMovement < 0f)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Rotate 180 degrees in Y-axis
            currentSprintTime += Time.deltaTime * sprintRecoveryWalk;
            currentSprintTime = currentSprintTime > sprintTime ? sprintTime : currentSprintTime;
        }
        else if (horizontalMovement > 0f)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotate 0 degrees in Y-axis
            currentSprintTime += Time.deltaTime * sprintRecoveryWalk;
            currentSprintTime = currentSprintTime > sprintTime ? sprintTime : currentSprintTime;
        }
        else
        {
            currentSprintTime += Time.deltaTime * sprintRecoveryStop;
            currentSprintTime = currentSprintTime > sprintTime ? sprintTime : currentSprintTime;
        }
        Slider();
    }

    private void Slider()
    {
        sprintSlider.fillAmount = currentSprintTime / sprintTime;
    }

    private void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        transform.position += (Vector3.right * horizontalMovement * currentSpeed * Time.fixedDeltaTime);
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

