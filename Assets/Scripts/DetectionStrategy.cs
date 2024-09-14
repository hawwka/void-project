using UnityEngine;

public class DetectionStrategy : IDetectionStrategy
{
    float detectionRange;

    public DetectionStrategy(float detectionRange)
    {
        this.detectionRange = detectionRange;
    }

    public bool Execute(Transform player, Transform enemy, Timer timer)
    {
        if (/*timer.IsRunning ||*/ !(Vector3.Distance(player.position, enemy.position) < detectionRange)) 
            return false;

        
        //timer.Run();
        
        return true;
    }
}