using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MeleeEnemyStatesFactory", menuName = "ScriptableObjects/Factories/Enemy/MeleeEnemyStatesFactory")]
public class MeleeEnemyStatesFactory : EnemyStatesFactory
{
    public override StateMachine Create(Enemy enemy)
    {
        var playerDetector = enemy.GetComponent<PlayerDetector>();
        var agent = enemy.GetComponent<NavMeshAgent>();
        
        var stateMachine = new StateMachine();

        var wanderState = new EnemyWanderState();
        var attackState = new EnemyAttackState(enemy);
        var chaseState = new EnemyChaseState(enemy, playerDetector, agent);

        stateMachine.AddState(wanderState);
        stateMachine.AddState(attackState);
        stateMachine.AddState(chaseState);
        
        stateMachine.AddTransition(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer(enemy, 20)));
        stateMachine.AddTransition(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer(enemy, 20)));
        stateMachine.AddTransition(chaseState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer(enemy, 3)));
        stateMachine.AddTransition(attackState, chaseState, new FuncPredicate(() => !playerDetector.CanAttackPlayer(enemy, 3)));

        stateMachine.SetState(wanderState);

        return stateMachine;
    }
}