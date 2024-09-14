using UnityEngine;

public class DetectionStrategy : IDetectionStrategy
{
    float detectionRange;
    float innerDetectionRange;

    public DetectionStrategy(float detectionRange)
    {
        this.detectionRange = detectionRange;
    }

    public bool Execute(Transform player, Transform enemy)
    {
        return Vector3.Distance(player.position, enemy.position) < detectionRange;
    }
}