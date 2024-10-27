using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyStatesFactory StatesFactory;
    public UnityEvent OnDamageTaken;
    
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