using UnityEngine;

[CreateAssetMenu(fileName = "MagazineReloadStrategy", menuName = "ScriptableObjects/Strategies/Weapon/MagazineReloadStrategy")]
public class MagazineReloadStrategy : ReloadStrategy
{
    Weapon weapon;
    Timer reloadTimer;

    public override bool IsReady => weapon.Magazine.Remaining > 0;
    public override bool IsReloading => reloadTimer.IsRunning;
    
    public override void Initialize(Weapon weapon)
    {
        this.weapon = weapon;
        reloadTimer = new Timer(weapon.Magazine.ReloadTime);
        reloadTimer.TimerFinished += FinishReload;
    }
    
    public override void Update()
    {
        reloadTimer.Tick(Time.deltaTime);
    }

    public override void Reload()
    {
        if (IsReloading)
            return;
        
        reloadTimer.Run();
        weapon.OnReload?.Invoke();
    }

    public override void Interrupt()
    {
        reloadTimer.Stop();
    }

    void FinishReload()
    {
        weapon.Magazine.Remaining = weapon.Magazine.Capacity;
    }
}
