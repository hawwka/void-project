using UnityEngine;

public class EnemyMelee : Enemy
{
    public int Damage = 20;
    public float AttackRange = 2f;
    
    Timer attackTimer;

    // buffer for Physics.OverlapSphereNonAlloc
    static Collider[] Results = new Collider[20]; 
    

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

         var count = Physics.OverlapSphereNonAlloc(transform.position, AttackRange, Results, LayerMask.GetMask("Player"));

         for (int i = 0; i < count; i++)
         {
             if (!Results[i].gameObject.TryGetComponent<Health>(out var health))
                 continue;
                     
             health.AddHealth(-Damage);
         }
    }

    public override void Die()
    {
        Destroy(this);
        Destroy(NavMeshAgent);
    }
}