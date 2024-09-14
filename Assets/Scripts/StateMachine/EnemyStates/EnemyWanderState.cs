using UnityEngine;

public class EnemyWanderState : IState
{
    Enemy enemy;
    Transform player;
    
    public EnemyWanderState(Enemy enemy, Transform player)
    {
        this.enemy = enemy;
        this.player = player;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void Update()
    {
        enemy.transform.LookAt(new Vector3(player.position.x, enemy.transform.position.y, player.position.z));
    }

    public void FixedUpdate()
    {
        
    }

}