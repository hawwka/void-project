using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunAttackStrategy", menuName = "ScriptableObjects/Strategies/Weapon/ShotgunAttackStrategy")]
public class ShotgunAttackStrategy : AttackStrategy
{
    public float AttackSpeed;
    public float Recoil;
    public float Range;
    public float Speed;
    public int PelletsPerShot = 10;
    public int Damage;
    
    Weapon weapon;
    float lastAttackedTime;
    
    public override bool CanAttack => Time.time - lastAttackedTime >= AttackSpeed;


    public override void Initialize(Weapon weapon)
    {
        this.weapon = weapon;
        lastAttackedTime = 0;
    }

    public override void Attack(Transform origin, Vector3 direction)
    {
        lastAttackedTime = Time.time;

        var recoilStep = Recoil * 2 / PelletsPerShot;
        var recoilCurrentPos = -Recoil;
        var dirVar = direction;

        for (int i = 0; i <= PelletsPerShot; i++)
        {
            var randomOffset = Random.insideUnitCircle * (recoilCurrentPos + recoilStep);
            
            direction = new Vector3(randomOffset.x, 0, randomOffset.y) + dirVar;
            recoilCurrentPos += recoilStep;

            if (!Physics.Raycast(origin.position, direction, out var hit, Range))
            {
                weapon.VisualEffect.ShowTracer(origin.position, direction, Range, Speed);
                continue;
            }
            
            if (hit.collider.TryGetComponent<Health>(out var targetHealthComponent))
            {
                targetHealthComponent.AddHealth(-Damage / PelletsPerShot);
            }

            weapon.VisualEffect.ShowTracer(origin.position, direction, hit.distance, Speed);
        }
    }
}