using UnityEngine;
using UnityEngine.UI;

public class ObjectMovement : MonoBehaviour
{
    [Header("General Settings")]
    public float normalSpeed = 2f;
    public float sprintSpeed = 4f;
    public float sprintCooldown = 2f;

    [Header("Sprint Settings")]
    [SerializeField] private float sprintTime = 10f; // Sprint Duration
    [SerializeField] private float sprintRecoveryStop = 1f;
    [SerializeField] private float sprintRecoveryWalk = 1f;
    [SerializeField] private Image sprintSlider;

    [Header("Animator")]
    public Animator animator;

    private SpriteRenderer spriteRenderer;
    private float currentSpeed;
    private float currentSprintTime;
    private float sprintCooldownTimer;
    private bool isCooldown = false;
    private bool isSprinting = false; // Campo privado
    public LedgeClimb lc { get; set; }

    private bool isPlayerStopped = false;

    // Propiedades públicas
    public float SprintDuration => sprintTime; // Duración del sprint
    public bool IsSprinting => isSprinting;   // Propiedad de solo lectura para "isSprinting"

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSprintTime = sprintTime;
        currentSpeed = normalSpeed;
        UpdateSprintSlider();
    }

    private void Update()
    {
        bool isWalking = Input.GetAxisRaw("Horizontal") != 0;
        bool isSprintingKeyPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Actualiza la lógica del sprint y obtiene la velocidad actual
        UpdateSprint(isWalking, isSprintingKeyPressed);

        // Maneja el movimiento del jugador
        HandleMovement(isWalking);
    }

    private void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (!isPlayerStopped)
        {
            transform.position += Vector3.right * horizontalMovement * currentSpeed * Time.fixedDeltaTime;
        }
    }

    private void UpdateSprint(bool isWalking, bool isSprintingKeyPressed)
    {
        if (!isCooldown && isSprintingKeyPressed && currentSprintTime > 0)
        {
            if (!isSprinting)
            {
                isSprinting = true;
            }

            currentSpeed = sprintSpeed;
            currentSprintTime -= Time.deltaTime;

            if (currentSprintTime <= 0f)
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
        }
        else if (isWalking)
        {
            currentSprintTime += Time.deltaTime * sprintRecoveryWalk;
        }

        currentSprintTime = Mathf.Min(currentSprintTime, sprintTime);
        UpdateSprintSlider();
    }

    private void HandleMovement(bool isWalking)
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (horizontalMovement < 0f)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            animator.SetBool("IsWalking", true);
        }
        else if (horizontalMovement > 0f)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void UpdateSprintSlider()
    {
        if (sprintSlider != null)
        {
            sprintSlider.fillAmount = currentSprintTime / sprintTime;
        }
    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool("IsWalking", isWalking);
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
