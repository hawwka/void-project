using UnityEngine;
using UnityEngine.AI;


public class EnemyChaseState : EnemyState
{
    Enemy enemy;
    PlayerDetector playerDetector;
    NavMeshAgent agent;

    public EnemyChaseState() { }

    public EnemyChaseState(Enemy enemy,  PlayerDetector playerDetector, NavMeshAgent agent)
    {
        this.enemy = enemy;
        this.agent = agent;
        this.playerDetector = playerDetector;
    }
    

    public override void Update()
    {
        enemy.transform.LookAt(new Vector3(player.transform.position.x, enemy.transform.position.y, player.transform.position.z));

        var dir = player.transform.position - enemy.transform.position;
        
        agent.SetDestination(player.position - dir.normalized * playerDetector.AttackRange);
    }

}