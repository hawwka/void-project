using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Weapon : MonoBehaviour
{
    public WeaponConfigSO WeaponConfigSo;

    [SerializeField] 
    public Transform WeaponSocket;
    
    [SerializeField] 
    protected WeaponVisualEffect visualEffect;

    [SerializeField] 
    protected float reloadTime;

    [SerializeField] 
    public int MagazineCapacity;
     
    Timer reloadTimer;
    public int ShotsInMagazine;

    protected void Start()
    {
        reloadTimer = new Timer(reloadTime);
        ShotsInMagazine = MagazineCapacity;
        reloadTimer.TimerFinished += OnReloadFinished;
    }

    public void Update()
    {
        reloadTimer.Tick(Time.deltaTime);
    }

    public abstract void Attack();

    public void Reload()
    {
        reloadTimer.Run();
    }

    void OnReloadFinished()
    {
        ShotsInMagazine = MagazineCapacity;
    }

    bool IsMagazineEmpty()
    {
        return ShotsInMagazine <= 0;
    }

    public bool IsAttackAllowed()
    {
        return (!IsMagazineEmpty() && !reloadTimer.IsRunning);
    }

}