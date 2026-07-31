using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 7f;
    public float gravity = -20f;

    [Header("Sprint")]
    public float maxStamina = 10f;
    public float staminaRecovery = 1.25f;
    public float staminaDrain = 1.2f;
    private bool isSprinting = false;

    [Header("Movement Smoothing")]
    public float movementSmoothTime = 0.08f;

    private Vector3 currentVelocity;
    private Vector3 velocityRef;

    [Header("Modifiers")]
    [Tooltip("Multiplier for sprint speed. Can be changed by gameplay.")]
    public float sprintSpeedMultiplier = 1f;

    [Tooltip("Multiplier for stamina recovery.")]
    public float staminaRecoveryMultiplier = 1f;

    [Header("Head Bob")]
    public Transform cameraHolder;
    public float bobSpeed = 10f;
    public float bobAmount = 0.05f;

    private CharacterController controller;

    private Vector3 velocity;
    private float currentStamina;
    private float defaultCamY;
    private float bobTimer;

    public float CurrentStamina => currentStamina;
    public float StaminaPercent => currentStamina / maxStamina;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        currentStamina = maxStamina;

        if (cameraHolder != null)
            defaultCamY = cameraHolder.localPosition.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        HandleSprint();
        HeadBob();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        float speed = isSprinting ? sprintSpeed * sprintSpeedMultiplier : walkSpeed;

        //controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 targetVelocity = (transform.right * x + transform.forward * z).normalized * speed;

        currentVelocity = Vector3.SmoothDamp(
            currentVelocity,
            targetVelocity,
            ref velocityRef,
            movementSmoothTime
        );

        controller.Move((currentVelocity + velocity) * Time.deltaTime);

        //controller.Move((velocity * Time.deltaTime) + (move * speed * Time.deltaTime));
    }

    void HandleSprint()
    {
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") > 0 && controller.velocity.magnitude > 0.1f;

        // Start sprinting only if enough stamina.
        if (!isSprinting && wantsToSprint && currentStamina >= 3f)
        {
            isSprinting = true;
        }

        // Stop sprinting if the player lets go or runs out.
        if (isSprinting)
        {
            currentStamina -= staminaDrain * Time.deltaTime;

            if (!wantsToSprint || currentStamina <= 0f)
            {
                isSprinting = false;
                currentStamina = Mathf.Max(0f, currentStamina);
            }
        }
        else
        {
            currentStamina += staminaRecovery * staminaRecoveryMultiplier * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        //Debug.Log("Stamina: " + currentStamina);
    }

    void HeadBob()
    {
        if (cameraHolder == null)
            return;

        //Debug.Log("Is grounded: " + controller.isGrounded + "\nVelocity: " + controller.velocity.magnitude);
        if (controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            //Debug.Log("Bobbing");
            bobTimer += Time.deltaTime * bobSpeed;

            float amount = bobAmount;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                amount *= 1.5f;
            }

            Vector3 pos = cameraHolder.localPosition;

            pos.y = defaultCamY + Mathf.Sin(bobTimer) * amount;

            cameraHolder.localPosition = pos;
        }
        else
        {
            //Debug.Log("Not bobbing");
            bobTimer = 0;

            Vector3 pos = cameraHolder.localPosition;
            pos.y = Mathf.Lerp(pos.y, defaultCamY, Time.deltaTime * 8f);

            cameraHolder.localPosition = pos;
        }
    }

    // Future upgrades can call these.

    public void SetSprintMultiplier(float value)
    {
        sprintSpeedMultiplier = value;
    }

    public void SetRecoveryMultiplier(float value)
    {
        staminaRecoveryMultiplier = value;
    }

    public void RestoreStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
    }
}