using UnityEditor.Experimental;
using UnityEngine;

public class EnemyAttackState : IState
{
    Enemy enemy;
    PlayerDetector playerDetector;

    public EnemyAttackState(){}
    
    public EnemyAttackState(Enemy enemy, PlayerDetector playerDetector)
    {
        this.enemy = enemy;
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

        enemy.Attack();
    }

    public void FixedUpdate()
    {

    }
}