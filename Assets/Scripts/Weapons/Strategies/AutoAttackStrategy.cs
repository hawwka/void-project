using UnityEngine;

[CreateAssetMenu(fileName = "AutoAttackStrategy", menuName = "Weapon/Strategies/AutoAttackStrategy")]
public class AutoAttackStrategy: AttackStrategy
{
    public float AttackSpeed;
    public float Recoil;
    public float Range;
    public int Damage;
    
    
    float lastAttackedTime;
    
    public override bool CanAttack => Time.time - lastAttackedTime >= AttackSpeed;
    
    public override void Initialize()
    {
        lastAttackedTime = 0;
    }

     public override void Attack(Transform origin, Vector3 direction)
     {
         lastAttackedTime = Time.time;
         
         var randomOffset = Random.insideUnitCircle * Recoil;
         
         direction += new Vector3(randomOffset.x, 0, randomOffset.y);
         
         if (!Physics.Raycast(origin.position, direction, out var hit, Range))
         {
             // visualEffect.ShowTracer(origin, dir, WeaponConfig.Range, 200f);
             return;
         }

         if (hit.collider.TryGetComponent<EnemyBase>(out var targetComponent))
             targetComponent.TakeDamage();
         if (hit.collider.TryGetComponent<Health>(out var targetHealthComponent))
             targetHealthComponent.TakeDamage(Damage);
         
         // visualEffect.ShowTracer(origin, dir, hit.distance, 200f);
     }
}
