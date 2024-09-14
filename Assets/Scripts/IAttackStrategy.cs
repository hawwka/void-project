using UnityEngine;


public interface IEnemyAttackStrategy
{
    public void Execute(Transform player, Transform enemy, Timer timer);
}

public interface IDetectionStrategy
{
    public bool Execute(Transform player, Transform enemy);
}