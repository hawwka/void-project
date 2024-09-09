using System;
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

    private Vector3 _input;
    private Vector3 _mouseInput;

    private Rigidbody rb;
    private float currentSpeed;
    private float lastDashTime;

    private bool canMove = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.freezeRotation = true;
    }

    private void OnEnable()
    {
        input.Dash += OnDash;
    }

    private void OnDisable()
    {
        input.Dash -= OnDash;
    }

    private void Update()
    {
        GatherInput();
        Look();
    }
    
    private void FixedUpdate()
    {
        if (_input.magnitude > 0) 
            Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    
    private void Look()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            var lookPos = Helpers.MouseToWorldPostion();
            
            lookPos.y = model.position.y;

            model.LookAt(lookPos);
            return;
        }
        
        if(_input == Vector3.zero)
            return;
        
        model.rotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
    }

    private void OnDash()
    {
        if (Time.time - lastDashTime < DelayAftetDash)
            return;
        
        canMove = false;
        
        lastDashTime = Time.time;
        
        if(_input == Vector3.zero)
            rb.AddForce(model.forward * dashForce, ForceMode.Impulse);
        else
            rb.AddForce(_input.normalized.ToIso() * dashForce, ForceMode.Impulse);
        
        Invoke(nameof(ResetDash), DashDuration);
    }

    private void ResetDash()
    {
        canMove = true;
    }
    
    private void Move()
    {
        if(!canMove)
            return;
        
        currentSpeed = baseSpeed;


        var normalizedInput = _input.normalized.ToIso();
        var forwardTransform = model.forward;
        
        var angle = Vector3.Angle(normalizedInput, forwardTransform);
        

        if (angle > 90 && angle <= 175)
            currentSpeed = baseSpeed * (360 - angle) / 360;
        else if (angle > 175 && angle <= 180)
            currentSpeed = (float)(baseSpeed * 0.5);
        
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            rb.MovePosition(transform.position + normalizedInput * (currentSpeed * Time.deltaTime));
        else
            rb.MovePosition(transform.position + forwardTransform * (currentSpeed * Time.deltaTime));
        // if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        //     rb.linearVelocity = (normalizedInput * (currentSpeed * Time.fixedDeltaTime));
        // else
        //     rb.linearVelocity = (forwardTransform * (currentSpeed * Time.fixedDeltaTime));
    }
}