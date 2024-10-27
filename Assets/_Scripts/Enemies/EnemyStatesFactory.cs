using UnityEngine;

public abstract class EnemyStatesFactory : ScriptableObject
{
    public abstract StateMachine Create(Enemy enemy);
}