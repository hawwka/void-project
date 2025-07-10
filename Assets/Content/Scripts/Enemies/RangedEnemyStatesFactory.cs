using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "RangedEnemyStatesFactory", menuName = "ScriptableObjects/Factories/Enemy/RangedEnemyStatesFactory")]
public class RangedEnemyStatesFactory : EnemyStatesFactory
{
    [SerializeField]
    EnemyProjectile projectilePrefab;
    
    
    public override StateMachine Create(Enemy enemy)
    {
        var playerDetector = enemy.GetComponent<PlayerDetector>();
        var agent = enemy.GetComponent<NavMeshAgent>();
        
        var stateMachine = new StateMachine();

        var wanderState = new EnemyWanderState();
        var attackState = new EnemyRangedAttackState(enemy, playerDetector, new Timer(enemy.AttackRate), projectilePrefab);
        var chaseState = new EnemyChaseState(enemy, playerDetector, agent);
        // var takeDamageState = new EnemyTakeDamageState(enemy);

        stateMachine.AddState(wanderState);
        stateMachine.AddState(attackState);
        stateMachine.AddState(chaseState);
        
        stateMachine.AddTransition(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
        stateMachine.AddTransition(attackState, chaseState, new FuncPredicate(() => !playerDetector.CanAttackPlayer()));

        // stateMachine.AddAnyTransition(takeDamageState, new FuncPredicate(() => isDamageTaken));
        // stateMachine.AddTransition(takeDamageState, chaseState, new FuncPredicate(() => !isDamageTaken));

        stateMachine.SetState(wanderState);

        return stateMachine;
    }
}