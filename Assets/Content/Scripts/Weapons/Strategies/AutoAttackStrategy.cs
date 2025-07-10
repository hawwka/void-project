using UnityEngine;

[CreateAssetMenu(fileName = "AutoAttackStrategy", menuName = "ScriptableObjects/Strategies/Weapon/AutoAttackStrategy")]
public class AutoAttackStrategy: AttackStrategy
{
    public float Recoil;
    public float Range;
    public float Speed;
    public int Damage;

    Weapon weapon;
    
    
    public override void Initialize(Weapon weapon)
    {
        this.weapon = weapon;
    }

     public override void Attack(Transform origin, Vector3 direction)
     {
         var randomOffset = Random.insideUnitCircle * Recoil;
         
         direction += new Vector3(randomOffset.x, 0, randomOffset.y);

         if (!Physics.Raycast(origin.position, direction, out var hit, Range))
         {
             weapon.VisualEffect.ShowTracer(origin.position, direction, Range, Speed);
             return;
         }

         if (hit.collider.TryGetComponent<Health>(out var targetHealthComponent))
         {
             targetHealthComponent.AddHealth(-Damage);
         }
         weapon.VisualEffect.ShowTracer(origin.position, direction, hit.distance, Speed);
     }
}
