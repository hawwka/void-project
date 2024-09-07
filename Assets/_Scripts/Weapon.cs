using UnityEngine;

public class Weapon : MonoBehaviour
{
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
            return;

        if (hit.collider.TryGetComponent<Enemy>(out var component))
            component.TakeDamage(Damage);    
        
        Debug.DrawRay(origin, dir * hit.distance, Color.yellow);
        
    }
}
