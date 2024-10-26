using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunAttackStrategy", menuName = "Weapon/Strategies/ShotgunAttackStrategy")]
public class ShotgunAttackStrategy : AttackStrategy
{
    public float AttackSpeed;
    public float Recoil;
    public float Range;
    public int PelletsPerShot = 10;
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
                // visualEffect.ShowTracer(origin, dir, WeaponConfig.Range, 200f);
                continue;
            }

            if (hit.collider.TryGetComponent<EnemyBase>(out var targetComponent))
                targetComponent.TakeDamage();
            if (hit.collider.TryGetComponent<Health>(out var targetHealthComponent))
                targetHealthComponent.TakeDamage((float)Damage / PelletsPerShot);

            // visualEffect.ShowTracer(origin, dir, hit.distance, 200f);
        }
        
        Debug.Log("Shotgun attacked!");
    }
}