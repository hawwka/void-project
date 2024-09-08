using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponConfigSO WeaponConfigSo;

    private float lastAttackedTime;

    public void Attack(Vector3 origin, Vector3 dir)
    {
        if (Time.time - lastAttackedTime < WeaponConfigSo.DelayAftetShot)
            return;

        lastAttackedTime = Time.time;

        
        Debug.DrawRay(origin, dir * WeaponConfigSo.Range, Color.red);

        if (!Physics.Raycast(origin, dir, out var hit, WeaponConfigSo.Range))
        {
            WeaponConfigSo.VisualEffect.ShowTracer(origin, dir, WeaponConfigSo.Range, 200f);
            return;
        }

        if (hit.collider.TryGetComponent<Enemy>(out var component))
            component.TakeDamage(WeaponConfigSo.Damage);
        
        WeaponConfigSo.VisualEffect.ShowTracer(origin, dir, hit.distance, 200f);

        Debug.DrawRay(origin, dir * hit.distance, Color.yellow);
    }
}