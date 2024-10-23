using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponConfigSO WeaponConfigSo;

    [SerializeField] 
    public Transform weaponSocket;
    
    [SerializeField] 
    protected WeaponVisualEffect visualEffect;

    [SerializeField] protected float reloadTime;

    [SerializeField] int magazineCapacity;
     
    Timer ReloadTimer;
    protected int shotsInMagazine;

    protected void Start()
    {
        ReloadTimer = new Timer(reloadTime);
        shotsInMagazine = magazineCapacity;
    }

    public void Update()
    {
        ReloadTimer.Tick(Time.deltaTime);
    }

    public abstract void Attack();

    public void Reload()
    {
        ReloadTimer.Run();
        shotsInMagazine = magazineCapacity;
    }

    bool IsMagazineEmpty()
    {
        return shotsInMagazine <= 0;
    }

    protected bool IsAttackAllowed()
    {
        return (!IsMagazineEmpty() && !ReloadTimer.IsRunning);
    }
}