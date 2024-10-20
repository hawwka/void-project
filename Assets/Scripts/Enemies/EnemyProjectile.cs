using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float velocityMagnitude = 5f;
    [SerializeField] float accelerationMagnitude = 1f;
    [SerializeField] float detonateRadius = .3f;
    [SerializeField] float LifeTime = 5f;
    [SerializeField] float damage = 10;

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
        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, velocityMagnitude);

        if (!lifeTimeTimer.IsRunning)
            Destroy(gameObject);
        
        if (!(Vector3.Distance(player.position, transform.position) <= detonateRadius))
            return;
        
        if (player.gameObject.TryGetComponent<Health>(out var component))
        {
            component.TakeDamage(damage);
            Destroy(gameObject);
        }
        
        Destroy(gameObject);
    }
}