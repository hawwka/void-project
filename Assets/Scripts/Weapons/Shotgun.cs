using UnityEngine;

public class Shotgun : Weapon
{
    private float lastAttackedTime;
    
    [SerializeField]
    private int pelletsAmount = 10;
    
    public override void Attack(Vector3 origin, Vector3 dir)
    {
        if (Time.time - lastAttackedTime < WeaponConfigSo.DelayAftetShot)
            return;

        lastAttackedTime = Time.time;

        var recoilStep = WeaponConfigSo.MaxRecoil * 2 / pelletsAmount;
        var recoilCurrentPos = -WeaponConfigSo.MaxRecoil;
        var dirx = dir.x;
        
        for (int i = 0; i <= pelletsAmount; i++)
        {
            dir.x = dirx + Random.Range(recoilCurrentPos, recoilCurrentPos + recoilStep);
            recoilCurrentPos += recoilStep;

            if (!Physics.Raycast(origin, dir, out var hit, WeaponConfigSo.Range))
            {
                visualEffect.ShowTracer(origin, dir, WeaponConfigSo.Range, 200f);
                continue;
            }

            if (hit.collider.TryGetComponent<Enemy>(out var component))
                component.TakeDamage(WeaponConfigSo.Damage);

            visualEffect.ShowTracer(origin, dir, hit.distance, 200f);
        }

    }
}
