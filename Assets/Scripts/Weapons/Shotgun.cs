using UnityEngine;

public class Shotgun : Weapon
{
    private float lastAttackedTime;
    
    [SerializeField]
    private int pelletsAmount = 10;
    
    public override void Attack()
    {
        if (Time.time - lastAttackedTime < WeaponConfigSo.DelayAftetShot)
            return;
        
        lastAttackedTime = Time.time;
        
        var origin = weaponSocket.position;
        var dir = weaponSocket.TransformDirection(Vector3.forward);
        var recoilStep = WeaponConfigSo.MaxRecoil * 2 / pelletsAmount;
        var recoilCurrentPos = -WeaponConfigSo.MaxRecoil;
        var dirVar = dir;
        
        for (int i = 0; i <= pelletsAmount; i++)
        {   
            var randomOffset = Random.insideUnitCircle * (recoilCurrentPos + recoilStep); 
            dir = new Vector3(randomOffset.x, 0, randomOffset.y) + dirVar;
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
