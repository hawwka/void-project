using UnityEngine;

public interface IAttackStrategy
{
    public bool Execute(Transform player, Transform enemy);
}