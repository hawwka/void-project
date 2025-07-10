using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    public float AttackRate = 1f;
    public EnemyStatesFactory StatesFactory;
    public UnityEvent OnDamageTaken;
    public NavMeshAgent NavMeshAgent;
    
    protected StateMachine StateMachine;
    
    
    void Awake()
    {
        StateMachine = StatesFactory.Create(this);
    }
    
    public virtual void Attack() { }

    public virtual void TakeDamage()
    {
        OnDamageTaken.Invoke();
    }

    public virtual void Die()
    {
        
    }
}