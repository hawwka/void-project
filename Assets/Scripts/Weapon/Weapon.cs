using UnityEngine;
using UnityEngine.Events;

public sealed class Weapon : MonoBehaviour, IAttackable, IReloadable, IEquippable
{
    public Transform Origin;
    public Transform Direction;
    
    [Tooltip("Between 0 and 1")]
    public float Ergonomic;
    
    public Magazine Magazine;
    
    public AttackStrategy AttackStrategy;
    public ReloadStrategy ReloadStrategy;
    
    public UnityEvent OnAttack = new();
    public UnityEvent OnReload = new();
    
    
    void Start()
    {
        ReloadStrategy.Initialize(this);
        AttackStrategy.Initialize();
    }
    
    public void Attack()
    {
        if (!AttackStrategy.CanAttack || !ReloadStrategy.IsReady)
            return;

        if (ReloadStrategy.IsReloading)
            ReloadStrategy.Interrupt();
        
        AttackStrategy.Attack(Origin, Direction.forward);
        
        Magazine.Remaining--;
        
        Debug.Log($"Remaining: {Magazine.Remaining}");
        OnAttack?.Invoke();
    }

    public void Reload()
    {
        ReloadStrategy.Reload();
    }

    public void Equip()
    {
        
    }

    public void Unequip()
    {
        
    }
}