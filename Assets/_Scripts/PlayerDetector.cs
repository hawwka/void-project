using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 10f; 
    [SerializeField] float attackRange = 2f;

    public float AttackRange => attackRange;
    
    public Transform Player { get; private set; }

    IDetectionStrategy detectionStrategy;
    
    void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    void Start()
    {
        detectionStrategy = new RadiusDetectionStrategy(detectionRadius);
    }
    
    public bool CanDetectPlayer()
    {
        return detectionStrategy.Execute(Player, transform);
    }
    
    public bool CanAttackPlayer()
    {
        return Vector3.Distance(Player.position, transform.position) < attackRange;
    }
}