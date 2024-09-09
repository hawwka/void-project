using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerInputReader input;
    [SerializeField] 
    private Transform model;
    
    [Header("Settings")]
    [SerializeField] 
    private float baseSpeed = 8;
    [SerializeField] 
    private float dashForce = 50;
    [SerializeField] 
    private float DelayAftetDash = 1f;
    [SerializeField] 
    private float DashDuration = 0.25f;

    private Vector3 movement;
    private Vector3 _mouseInput;

    private Rigidbody rb;
    private float currentSpeed;
    private float lastDashTime;

    private bool canMove = true;
    private bool isMouseAiming;
    
    private const float backwardSpeedFactor = 0.5f;
    private const float forwardMoveThreshold = 90f;
    private const float backwardMoveThreshold = 175f;
    private const float maxBackwardAngle = 180f;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.freezeRotation = true;
    }

    private void OnEnable()
    {
        input.Dash += OnDash;
        input.Aim += HandleAim;
    }

    private void OnDisable()
    {
        input.Dash -= OnDash;
        input.Aim -= HandleAim;
    }

    private void HandleAim(bool isRMousePressed)
    {
        isMouseAiming = isRMousePressed;
    }

    private void Update()
    {
        movement = new Vector3(input.Direction.x, 0, input.Direction.y);

        Look();
    }
    
    private void FixedUpdate()
    {
        if (movement.magnitude > 0) 
            Move();
    }

    private void Look()
    {
        if (isMouseAiming)
        {
            var lookPos = Helpers.MouseToWorldPostion(input.MousePosition);
            
            lookPos.y = model.position.y;

            model.LookAt(lookPos);
            return;
        }
        
        if (movement.magnitude < 0.1f)
            return;
        
        model.rotation = Quaternion.LookRotation(movement.ToIso(), Vector3.up);
    }

    private void OnDash()
    {
        if (Time.time - lastDashTime < DelayAftetDash)
            return;
        
        canMove = false;
        
        lastDashTime = Time.time;
        
        if(movement == Vector3.zero)
            rb.AddForce(model.forward * dashForce, ForceMode.Impulse);
        else
            rb.AddForce(movement.normalized.ToIso() * dashForce, ForceMode.Impulse);
        
        Invoke(nameof(ResetDash), DashDuration);
    }

    private void ResetDash()
    {
        canMove = true;
    }
    
    private void Move()
    {
        if (!canMove)
            return;
        
        currentSpeed = baseSpeed;


        var normalizedInput = movement.normalized.ToIso();
        var forwardTransform = model.forward;
        
        var angle = Vector3.Angle(normalizedInput, forwardTransform);
        
        
        if (angle > forwardMoveThreshold && angle <= backwardMoveThreshold)
        {
            currentSpeed = baseSpeed * (360 - angle) / 360;
        }
        else if (angle > backwardMoveThreshold && angle <= maxBackwardAngle)
        {
            currentSpeed = baseSpeed * backwardSpeedFactor;
        }

        
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            rb.MovePosition(transform.position + normalizedInput * (currentSpeed * Time.deltaTime));
        else
            rb.MovePosition(transform.position + forwardTransform * (currentSpeed * Time.deltaTime));
    }
}