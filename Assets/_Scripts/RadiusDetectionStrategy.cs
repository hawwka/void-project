using UnityEngine;

public class RadiusDetectionStrategy : IDetectionStrategy
{
    float detectionRange;
    float innerDetectionRange;

    public RadiusDetectionStrategy(float detectionRange)
    {
        this.detectionRange = detectionRange;
    }

    public bool Execute(Transform player, Transform enemy)
    {
        return Vector3.Distance(player.position, enemy.position) < detectionRange;
    }
}