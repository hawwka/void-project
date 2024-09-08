using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rifle : Weapon
{
    private float lastAttackedTime;
    
    public override void Attack(Vector3 origin, Vector3 dir)
    {
        if (Time.time - lastAttackedTime < WeaponConfigSo.DelayAftetShot)
            return;

        lastAttackedTime = Time.time;
        
        dir.x += Random.Range(-WeaponConfigSo.MaxRecoil, WeaponConfigSo.MaxRecoil);

        if (!Physics.Raycast(origin, dir, out var hit, WeaponConfigSo.Range))
        {
            visualEffect.ShowTracer(origin, dir, WeaponConfigSo.Range, 200f);
            return;
        }

        if (hit.collider.TryGetComponent<Enemy>(out var component))
            component.TakeDamage(WeaponConfigSo.Damage);
        
        visualEffect.ShowTracer(origin, dir, hit.distance, 200f);
    }
}
