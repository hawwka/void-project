using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : State
{
    Transform enemyTransform;
    NavMeshAgent agent;
    [SerializeField] List<Transform> trackPoints;

    int currentPoint = 1;
    bool reverseMovement = false;

    public override void OnEnter()
    {
        TryGetComponent(out enemyTransform);
        TryGetComponent(out agent);
        agent.SetDestination(trackPoints[1].position);
    }

    public override void OnExit()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void FixedUpdate()
    {
        if (Vector3.Distance(enemyTransform.position, trackPoints[currentPoint].position) < 2f)
        {
            agent.SetDestination(GetNextPoint());
        }
    }

    Vector3 GetNextPoint()
    {
        if (!reverseMovement)
        {
            currentPoint++;
            if (currentPoint >= trackPoints.Count)
            {
                currentPoint = trackPoints.Count - 1;
                reverseMovement = true;
            }
        }
        else
        {
            currentPoint--;
            if (currentPoint < 0)
            {
                currentPoint = 1;
                reverseMovement = false;
            }
        }
        
        return trackPoints[currentPoint].position;
    }
}