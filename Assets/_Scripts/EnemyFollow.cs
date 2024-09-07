using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Transform player;

    
    private void Update()
    {
        var offseet = (player.position - transform.position).normalized * 2;
        enemyAgent.SetDestination(player.position - offseet);
    }
}
