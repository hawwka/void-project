using UnityEngine;
using VContainer;

public class PlayerDetector : MonoBehaviour
{
    [Inject] Player player;
    
    public bool CanDetectPlayer(Enemy enemy, float detectionRadius)
    {
        return Vector3.Distance(player.transform.position, enemy.transform.position) < detectionRadius;
    }
    
    public bool CanAttackPlayer(Enemy enemy, float attackRadius)
    {
        return Vector3.Distance(player.transform.position, enemy.transform.position) < attackRadius;
    }
}