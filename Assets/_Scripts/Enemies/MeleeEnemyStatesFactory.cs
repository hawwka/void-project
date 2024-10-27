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
        var attackState = new EnemyAttackState(enemy, playerDetector.Player);
        var chaseState = new EnemyChaseState(enemy, playerDetector, agent);

        stateMachine.AddState(wanderState);
        stateMachine.AddState(attackState);
        stateMachine.AddState(chaseState);
        
        stateMachine.AddTransition(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
        stateMachine.AddTransition(attackState, chaseState, new FuncPredicate(() => !playerDetector.CanAttackPlayer()));

        stateMachine.SetState(wanderState);

        return stateMachine;
    }
}