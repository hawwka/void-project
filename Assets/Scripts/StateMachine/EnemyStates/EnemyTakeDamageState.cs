using UnityEngine;

public class EnemyTakeDamageState : IState
{
    EnemyBase enemy;

    public EnemyTakeDamageState(EnemyBase enemy)
    {
        this.enemy = enemy;
    }
    
    public void OnEnter()
    {
        enemy.ProcessDamageTaken();
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
