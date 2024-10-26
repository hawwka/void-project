using UnityEngine;

public abstract class ReloadStrategy : ScriptableObject
{
    public abstract bool IsReady { get; }
    
    public abstract bool IsReloading { get; }
    
    public abstract void Initialize(Weapon weapon);
    
    public abstract void Reload();
    
    public abstract void Interrupt();
}