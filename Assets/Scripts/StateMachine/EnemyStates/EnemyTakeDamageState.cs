using UnityEngine;

public class EnemyTakeDamageState : IState
{
    Enemy enemy;

    public EnemyTakeDamageState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    
    public void OnEnter()
    {
        enemy.TakeDamage();
        enemy.currentTakenDamage = 0;
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
