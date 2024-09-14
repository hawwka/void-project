using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : IState
{
    Enemy enemy;
    PlayerDetector playerDetector;
    NavMeshAgent agent;
    
    public EnemyChaseState(Enemy enemy,  PlayerDetector playerDetector, NavMeshAgent agent)
    {
        this.enemy = enemy;
        this.agent = agent;
        this.playerDetector = playerDetector;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Update()
    {
        var player = playerDetector.Player;
        
        enemy.transform.LookAt(new Vector3(player.position.x, enemy.transform.position.y, player.position.z));

        if (!playerDetector.CanDetectPlayer()) 
            return;
        
        var dir = player.position - enemy.transform.position;
        
        agent.SetDestination(player.position - dir.normalized * playerDetector.AttackRange);
    }

    public void FixedUpdate()
    {
        
    }
}