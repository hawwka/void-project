using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public sealed class Weapon : MonoBehaviour, IAttackable, IReloadable, IEquippable
{
    public Transform Origin;
    public Transform Direction;
    
    [Tooltip("Between 0 and 1")]
    public float Ergonomic;
    
    public Magazine Magazine;

    public WeaponVisualEffect VisualEffect;
    public AttackStrategy AttackStrategy;
    public ReloadStrategy ReloadStrategy;
    
    public UnityEvent OnAttack = new();
    public UnityEvent OnReload = new();
    
    
    void Start()
    {
        ReloadStrategy.Initialize(this);
        AttackStrategy.Initialize(this);
        Magazine.Remaining = Magazine.Capacity;
    }

    void Update()
    {
        ReloadStrategy.Update();
    }
    
    public void Attack()
    {
        if (!AttackStrategy.CanAttack || !ReloadStrategy.IsReady)
            return;

        if (ReloadStrategy.IsReloading)
            ReloadStrategy.Interrupt();
        
        AttackStrategy.Attack(Origin, Direction.forward);
        
        Magazine.Remaining--;
        
        OnAttack?.Invoke();
    }

    public void Reload()
    {
        ReloadStrategy.Reload();
    }

    public void Equip()
    {
        gameObject.GetComponentInChildren<Renderer>().enabled = true;
    }

    public void Unequip()
    {
        ReloadStrategy.Interrupt();
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
    }
}