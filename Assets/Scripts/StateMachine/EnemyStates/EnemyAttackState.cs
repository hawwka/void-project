using UnityEngine;

public class EnemyAttackState : IState
{
    Enemy enemy;
    Transform player;
    EnemyProjectile projectile;
    
    public EnemyAttackState(Enemy enemy, EnemyProjectile projectile, Transform player)
    {
        this.enemy = enemy;
        this.projectile = projectile;
        this.player = player;
    }

    public void OnEnter()
    {
        projectile =  Object.Instantiate(projectile, player.position + player.TransformDirection(Vector3.forward), Quaternion.identity);

        projectile.Init(player);
    }

    public void OnExit()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }
}