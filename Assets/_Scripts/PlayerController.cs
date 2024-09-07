using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    
    private Vector3 _input;
    private Vector3 _mouseInput;
    
    private Vector3 mouseWorldPos;
    
    
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
        if (_input == Vector3.zero && !Input.GetMouseButton(1)) return;

        if (Input.GetMouseButton(1))
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
        if (Input.GetMouseButton(1))
        {
            rb.MovePosition(transform.position + _input.normalized.ToIso() * (speed * Time.deltaTime));
        }
        else
            rb.MovePosition(transform.position + transform.forward * (speed * Time.deltaTime));
    }
}
