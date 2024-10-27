using UnityEngine;

public class EnemyMelee : Enemy
{
    public float AttackRate = 1f;

    Timer attackTimer;

    
    void Start()
    {
        attackTimer = new Timer(AttackRate);
    }
    
    void Update()
    {
        StateMachine.Update();

        attackTimer.Tick(Time.deltaTime);
    }

    void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }
    
    public override void Attack()
    {
        if (attackTimer.IsRunning)
            return;

        attackTimer.Run();
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}