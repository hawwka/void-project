using UnityEditor.Experimental;
using UnityEngine;
using VContainer;

public class EnemyAttackState : IState
{
    [Inject] Player player;
    
    Enemy enemy;
    
    
    public EnemyAttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
        
    }

    public void Update()
    {
        enemy.transform.LookAt(new Vector3(player.transform.position.x, enemy.transform.position.y, player.transform.position.z));
        enemy.Attack();
    }

    public void FixedUpdate()
    {

    }
}