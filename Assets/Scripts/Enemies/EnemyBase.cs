using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected abstract void Awake();

    protected virtual void Start()
    {
        SetupStateMachine();
    }

    protected abstract void SetupStateMachine();

    protected abstract void Update();

    protected abstract void FixedUpdate();

    public abstract void ProcessDamageTaken();

    public abstract void Attack();
    
    public abstract void TakeDamage();
    
    public abstract void Die();
    
    // Are called only from override methods
    // protected abstract IEnumerator TakeDamageRoutine();
}
