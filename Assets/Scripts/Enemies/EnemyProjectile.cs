using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float VelocityMagnitude = 5f;
    [SerializeField] float accelerationMagnitude = 1f;
    [SerializeField] float DetonateRadius = .3f;
    [SerializeField] float LifeTime = 5f;

    Transform player;
    Rigidbody rb;
    
    private Vector3 acceleration;

    private Timer lifeTimeTimer;
    
    
    public void Init(Transform player)
    {
        this.player = player;
    }

    private void Start()
    {
        lifeTimeTimer = new Timer(LifeTime);
        lifeTimeTimer.Run();

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        lifeTimeTimer.Tick(Time.fixedDeltaTime);
        
        var dir = (player.position - transform.position).normalized;
        
        dir.y = transform.position.y;

        acceleration = dir;
        acceleration *= accelerationMagnitude;

        rb.linearVelocity += acceleration;
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, VelocityMagnitude);

        if (!lifeTimeTimer.IsRunning)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out var component))
        {
            component.HandleDamage(10);
            Destroy(gameObject);
        }
    }
}