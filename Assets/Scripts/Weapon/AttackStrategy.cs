using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    public abstract bool CanAttack { get; }
    
    public abstract void Initialize();
    public abstract void Attack(Transform origin, Vector3 direction);
}