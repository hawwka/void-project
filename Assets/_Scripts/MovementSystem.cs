using System;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseSpeed = 8;
    
    private Vector3 _input;
    private Vector3 _mouseInput;
    
    private Vector3 _mouseWorldPos;
    
    
    private void Update()
    {
        GatherInput();
        Look();
    }
    
    private void FixedUpdate()
    {
        if (_input.magnitude > 0) Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    
    private void Look()
    {
        if (_input == Vector3.zero && !Input.GetMouseButton(1) && !Input.GetMouseButton(0)) return;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            var lookPos = Helpers.MouseToWorldPostion();
            
            lookPos.y = transform.position.y;

            transform.LookAt(lookPos);
            return;
        }
        
        transform.rotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
    }

    private void Move()
    {
        var normalizedInput = _input.normalized.ToIso();
        var forwardTransform = transform.forward;
        
        float angle = Vector3.Angle(normalizedInput, forwardTransform);
        float speed = baseSpeed;
        var absAngle = Mathf.Abs(angle);

        if (absAngle > 90 && absAngle <= 175)
            speed = baseSpeed * (360 - absAngle) / 360;
        else if (absAngle > 175 && absAngle <= 180)
            speed = (float)(baseSpeed * 0.5);
        
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            rb.MovePosition(transform.position + normalizedInput * (speed * Time.deltaTime));
        }
        else
            rb.MovePosition(transform.position + forwardTransform * (speed * Time.deltaTime));
    }
}