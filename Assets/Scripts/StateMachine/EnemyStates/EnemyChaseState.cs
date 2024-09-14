using UnityEngine;

public class EnemyChaseState : IState
{
    Enemy enemy;
    Transform player;
    
    public EnemyChaseState(Enemy enemy, Transform player)
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
        // enemy.transform.LookAt(new Vector3(player.position.x, enemy.transform.position.y, player.position.z));
        // enemy.transform.position += enemy.transform.forward * enemy.Speed * Time.deltaTime;

    }

    public void FixedUpdate()
    {
        
    }
}