using UnityEngine;
using VContainer;

public class EnemyRangedAttackState : IState
{
    Enemy enemy;
    PlayerDetector playerDetector;
    Timer attackTimer; 
    EnemyProjectile projectilePrefab;
    [Inject] Player player;
    
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
        
        
        enemy.transform.LookAt(new Vector3(player.transform. position.x, enemy.transform.position.y, player. transform.position.z));

        if (attackTimer.IsRunning)
            return;
        
        attackTimer.Run();
        
        var projectile = Object.Instantiate(projectilePrefab, enemy.transform.position + enemy.transform.TransformDirection(Vector3.forward), Quaternion.identity);
        
        projectile.Init(player.transform);
    }

    public void FixedUpdate()
    {
    }

    public void OnExit()
    {
    }

}