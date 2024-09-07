using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 360;
    private Vector3 _input;

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        if(_input.magnitude > 0) Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;
        
        // var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);

        // transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        
        transform.rotation = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
    }

    private void Move()
    {
        rb.MovePosition(transform.position + transform.forward * (speed * Time.deltaTime));
    }
}