using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rifle : Weapon
{
    private float lastAttackedTime;
    
    public override void Attack()
    {
        if (Time.time - lastAttackedTime < WeaponConfigSo.DelayAftetShot)
            return;

        lastAttackedTime = Time.time;
     
        var origin = weaponSocket.position;
        var dir = weaponSocket.TransformDirection(Vector3.forward);
        var randomOffset = Random.insideUnitCircle * WeaponConfigSo.MaxRecoil; 
        
        dir += new Vector3(randomOffset.x, 0, randomOffset.y);
        
        if (!Physics.Raycast(origin, dir, out var hit, WeaponConfigSo.Range))
        {
            visualEffect.ShowTracer(origin, dir, WeaponConfigSo.Range, 200f);
            return;
        }

        if (hit.collider.TryGetComponent<EnemyBase>(out var targetComponent))
            targetComponent.TakeDamage();
        if (hit.collider.TryGetComponent<HealthController>(out var targetHealthComponent))
            targetHealthComponent.TakeDamage(WeaponConfigSo.Damage);
        
        visualEffect.ShowTracer(origin, dir, hit.distance, 200f);
    }
}
