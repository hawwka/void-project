using UnityEngine;

public class EnemyRangedAttackState : IState
{
    Enemy enemy;
    PlayerDetector playerDetector;
    Timer attackTimer; 
    EnemyProjectile projectilePrefab;
    
    
    public EnemyRangedAttackState(Enemy enemy, PlayerDetector playerDetector, Timer attackTimer, EnemyProjectile projectilePrefab)
    {
        this.enemy = enemy;
        this.playerDetector = playerDetector;
        this.attackTimer = attackTimer;
        this.projectilePrefab = projectilePrefab;
    }

    public  void OnEnter()
    {
        attackTimer.Run();
    }


    public  void Update()
    {
        attackTimer.Tick(Time.deltaTime);
        
        if (attackTimer.IsRunning)
            return;
            
        var player = playerDetector.Player;
        
        enemy.transform.LookAt(new Vector3(player.position.x, enemy.transform.position.y, player.position.z));

        if (attackTimer.IsRunning)
            return;
        
        attackTimer.Run();
        
        var projectile = Object.Instantiate(projectilePrefab, enemy.transform.position + enemy.transform.TransformDirection(Vector3.forward), Quaternion.identity);
        
        projectile.Init(playerDetector.Player);
    }

    public void FixedUpdate()
    {
    }

    public void OnExit()
    {
    }

}