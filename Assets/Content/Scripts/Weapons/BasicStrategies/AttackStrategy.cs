using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    public abstract void Initialize(Weapon weapon);
    public abstract void Attack(Transform origin, Vector3 direction);
}