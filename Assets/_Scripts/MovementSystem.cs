using System;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float baseSpeed = 8;
    
    private Vector3 _input;
    private Vector3 _mouseInput;
    
    private Vector3 _mouseWorldPos;
    private float currentSpeed;

    private GUIStyle guiStyle = new GUIStyle();
    
    void Start()
    {
        // Настраиваем стиль текста
        guiStyle.fontSize = 24; // Устанавливаем размер шрифта (например, 24)
        guiStyle.normal.textColor = Color.green; // Цвет текста
    }
    
    private void OnGUI()
    {
        // Отображаем текст на экране с помощью метода OnGUI
        GUI.Label(new Rect(10, 10, 200, 20), "currentSpeed: " + currentSpeed .ToString("F2"), guiStyle);
        
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
        else
            currentSpeed = 0;
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
            
            lookPos.y = transform.position.y;

            transform.LookAt(lookPos);
            return;
        }
        
        if(_input == Vector3.zero)
            return;
        
        transform.rotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
    }

    private void Move()
    {
        currentSpeed = baseSpeed;


        var normalizedInput = _input.normalized.ToIso();
        var forwardTransform = transform.forward;
        
        var angle = Vector3.Angle(normalizedInput, forwardTransform);
        

        if (angle > 90 && angle <= 175)
            currentSpeed = baseSpeed * (360 - angle) / 360;
        else if (angle > 175 && angle <= 180)
            currentSpeed = (float)(baseSpeed * 0.5);
        
        
        
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            rb.MovePosition(transform.position + normalizedInput * (currentSpeed * Time.deltaTime));
        else
            rb.MovePosition(transform.position + forwardTransform * (currentSpeed * Time.deltaTime));
    }
}