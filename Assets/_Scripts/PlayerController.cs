using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody  rb;
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
        Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        

        if(_input.x != 0 || _input.y != 0) Debug.Log(_input);
    }

    private void Look()
    {
        if (_input != Vector3.zero)
        {
            var relative = (transform.position + _input) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        rb.MovePosition(transform.position + transform.forward * (_input.magnitude * (speed * Time.deltaTime)));
    }
}
