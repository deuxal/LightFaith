using UnityEngine;
using UnityEngine.UI;

public class ObjectMovement : MonoBehaviour
{
    public HealthSystemAttribute hsa;
    public Collider2D myCollider;
    public LedgeClimb lc;
    public float normalSpeed = 2f;
    public float sprintSpeed = 4f;
    public float sprintDuration = 5f;
    public float sprintCooldown = 2f;
    public Animator animator;
    private bool isClimbing = false;
    private bool isInitialized = false;

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

    // Step sound variables
    public AudioSource stepSoundSource1;
    public AudioSource stepSoundSource2;
    private float stepSoundSpeedMultiplier = 1f; // Adjust this value to control the step sound speed

    private void Start()
    {
        currentSprintTime = sprintTime;
        currentSpeed = normalSpeed;
        sprintTimer = sprintDuration;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the AudioSource components
        stepSoundSource1 = gameObject.AddComponent<AudioSource>();
        stepSoundSource2 = gameObject.AddComponent<AudioSource>();

        // Set loop and volume properties for step sounds
        stepSoundSource1.loop = true;
        stepSoundSource1.volume = 0.5f;
        stepSoundSource2.loop = true;
        stepSoundSource2.volume = 0.5f;

        Slider();
    }

    private void Update()
    {
        if (hsa.health <= 0)
        {
            animator.SetBool("IsDeath", true);
            this.enabled = false;
        }
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
            lc.MoveToPoint(this.transform, () => { StartLedgeClimb(); }, () => { EndLedgeClimb(); });
        }

        // Rotate the sprite based on the input keys
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (horizontalMovement < 0f)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Rotate 180 degrees in Y-axis
            currentSprintTime += Time.deltaTime * sprintRecoveryWalk;
            currentSprintTime = currentSprintTime > sprintTime ? sprintTime : currentSprintTime;
            // Set the walking animation parameter to true
            animator.SetBool("IsWalking", true);
        }
        else if (horizontalMovement > 0f)
        {
            spriteRenderer.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotate 0 degrees in Y-axis
            currentSprintTime += Time.deltaTime * sprintRecoveryWalk;
            currentSprintTime = currentSprintTime > sprintTime ? sprintTime : currentSprintTime;
            // Set the walking animation parameter to true
            animator.SetBool("IsWalking", true);
        }
        else
        {
            currentSprintTime += Time.deltaTime * sprintRecoveryStop;
            currentSprintTime = currentSprintTime > sprintTime ? sprintTime : currentSprintTime;
            // Set the walking animation parameter to false
            animator.SetBool("IsWalking", false);
        }

        // Update step sound pitch based on player's speed
        float pitchMultiplier = currentSpeed * stepSoundSpeedMultiplier;
        stepSoundSource1.pitch = pitchMultiplier;
        stepSoundSource2.pitch = pitchMultiplier;

        Slider();

        if (!isInitialized) // Check if the script is initialized
        {
            isInitialized = true;
            return; // Skip the animation control on the first frame
        }
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

    public void StartLedgeClimb()
    {
        animator.SetBool("IsClimbing", true);
        myCollider.enabled = false;
        isClimbing = false;
    }

    public void EndLedgeClimb()
    {
        animator.SetBool("IsClimbing", false);
        myCollider.enabled = true;
        isClimbing = true;
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


