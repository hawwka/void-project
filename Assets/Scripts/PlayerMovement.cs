using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerInputReader input;
    [SerializeField] Transform model;
    
    [Header("Settings")]
    [SerializeField] float baseSpeed = 8;
    [SerializeField] float dashForce = 50;
    [SerializeField] float DelayAftetDash = 1f;
    [SerializeField] float DashDuration = 0.25f;
    [SerializeField] float AimingSpeedPenaltyFactor = 0.4f;


    private Vector3 movement;
    private Vector3 _mouseInput;

    private Rigidbody rb;
    private float currentSpeed;
    private float lastDashTime;

    private bool canMove = true;
    private bool isMouseAiming;
    
    
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
        
        var normalizedInput = movement.normalized.ToIso();
        var forwardTransform = model.forward;

        if (isMouseAiming)
        {
            rb.MovePosition(transform.position + normalizedInput * (baseSpeed * AimingSpeedPenaltyFactor * Time.deltaTime));
            return;
        }
     
        rb.MovePosition(transform.position + forwardTransform * (baseSpeed * Time.deltaTime));
    }
}