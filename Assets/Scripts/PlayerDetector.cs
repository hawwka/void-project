using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 10f; 
    [SerializeField] float attackRange = 2f;

    public Transform Player { get; private set; }

    IDetectionStrategy detectionStrategy;
    
    void Awake()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }
    
    public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) // TODO Пробрасывать через DI контейнер
    {
        this.detectionStrategy = detectionStrategy;
    }
    
    public bool CanDetectPlayer()
    {
        return Vector3.Distance(Player.position, transform.position) < detectionRadius;
    }
    
    public bool CanAttackPlayer()
    {
        return Vector3.Distance(Player.position, transform.position) < attackRange;
    }
}