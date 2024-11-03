using UnityEngine;

public abstract class ReloadStrategy : ScriptableObject
{
    public abstract void Initialize(Weapon weapon);
    
    public abstract void Update();
    
    public abstract bool IsReloading { get; }
    
    public abstract void Reload();
    
    public abstract void Interrupt();
}