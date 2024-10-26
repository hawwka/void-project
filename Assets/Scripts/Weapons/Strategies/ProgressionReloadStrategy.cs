using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "InterruptedReloadStrategy", menuName = "Weapon/Strategies/InterruptedReloadStrategy")]
public class ProgressionReloadStrategy : ReloadStrategy
{
    Weapon weapon;
    Coroutine reloadRoutine;

    public override bool IsReady => weapon.Magazine.Remaining > 0;
    public override bool IsReloading => reloadRoutine != null;

    public override void Initialize(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public override void Update() { }
    
    public override void Reload()
    {
        if (reloadRoutine != null)
            return;

        reloadRoutine = weapon.StartCoroutine(ReloadRoutine());
    }

    public override void Interrupt()
    {
        if (reloadRoutine != null)
        {
            weapon.StopCoroutine(reloadRoutine);
            reloadRoutine = null;
        }

    }

    IEnumerator ReloadRoutine()
    {
        var delay = new WaitForSeconds(weapon.Magazine.ReloadTime);
        
        while (weapon.Magazine.Remaining < weapon.Magazine.Capacity)
        {
            weapon.OnReload?.Invoke();
            
            yield return delay;
            
            weapon.Magazine.Remaining++;
        }
    }
}