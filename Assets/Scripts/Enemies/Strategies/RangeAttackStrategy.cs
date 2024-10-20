using UnityEngine;

public class RangeAttackStrategy : IAttackStrategy
{
    float attackRange;
    float attackSpeed;

    public RangeAttackStrategy(float attackRange, float attackSpeed)
    {
        this.attackRange = attackRange;
        this.attackSpeed = attackSpeed;
    }

    public bool Execute(Transform player, Transform enemy)
    {
        return Vector3.Distance(player.position, enemy.position) < attackRange;
    }
}