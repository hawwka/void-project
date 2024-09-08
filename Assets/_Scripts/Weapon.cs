using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField, Header("References")]
    private WeaponVisualEffect VisualEffect;
    
    [SerializeField]
    private float ChargeTime, DelayAftetShot, Range;

    [SerializeField]
    private int Damage;

    private float lastAttackedTime;
    

    public void Attack(Vector3 origin, Vector3 dir)
    {
        if (Time.time - lastAttackedTime < DelayAftetShot)
            return;

        lastAttackedTime = Time.time;

        
        Debug.DrawRay(origin, dir * Range, Color.red);

        if (!Physics.Raycast(origin, dir, out var hit, Range))
        {
<<<<<<< HEAD
            VisualEffect.ShowTracer(origin, dir, Range, 200f);
=======
            VisualEffect.ShowTracer(origin, dir, Range, 100f);
>>>>>>> c0f8f54bf8935944e254e14ac9c6ffa89a58ffc4
            return;
        }

        if (hit.collider.TryGetComponent<Enemy>(out var component))
            component.TakeDamage(Damage);
        
<<<<<<< HEAD
        VisualEffect.ShowTracer(origin, dir, hit.distance, 200f);
=======
        VisualEffect.ShowTracer(origin,  dir, Vector3.Distance(origin, hit.point), 100f);
>>>>>>> c0f8f54bf8935944e254e14ac9c6ffa89a58ffc4


        Debug.DrawRay(origin, dir * hit.distance, Color.yellow);
    }
}