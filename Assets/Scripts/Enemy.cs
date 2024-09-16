using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Renderer objectRenderer;
    [SerializeField] EnemyProjectile projectilePrefab;
    
    [Header("Settings")]
    [SerializeField] int maxHealth = 100;
    [SerializeField] float attackCooldown = 1f;
    
    IAttackStrategy attackStrategy; 
    
    PlayerDetector playerDetector;
    NavMeshAgent agent;
    int currentHealth;
    Color defaultColor;
    
    Timer attackTimer;

    StateMachine stateMachine;

    public int currentTakenDamage;
    public bool playerDetected = false;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerDetector = GetComponent<PlayerDetector>();
    }

    private void Start()
    {
        SetupStateMachine();
        
        currentHealth = maxHealth; // TODO: сделать отдельный компонент отвечающий за хп сущностей и убрать это отсюда
        objectRenderer = GetComponent<Renderer>();
        defaultColor = objectRenderer.material.color;

        attackTimer = new Timer(attackCooldown);
    }

    void SetupStateMachine()
    {
        stateMachine = new StateMachine();

        var wanderState = new EnemyWanderState();
        var attackState = new EnemyAttackState(this, playerDetector.Player);
        var chaseState = new EnemyChaseState(this, playerDetector, agent);
        var takeDamageState = new EnemyTakeDamageState(this);

        stateMachine.AddTransition(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
        stateMachine.AddTransition(chaseState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
        stateMachine.AddTransition(attackState, chaseState, new FuncPredicate(() => !playerDetector.CanAttackPlayer()));
        
        stateMachine.AddAnyTransition(takeDamageState, new FuncPredicate(() => IsTakingDamage()));
        stateMachine.AddTransition(takeDamageState, chaseState, new FuncPredicate(() => !IsTakingDamage()));

        stateMachine.SetState(wanderState);
    }

    private void Update()
    {
        stateMachine.Update();
        
        attackTimer.Tick(Time.deltaTime);
    }
    
    
    private void FixedUpdate() => stateMachine.FixedUpdate();

    public void SetDamage(int damage)
    {
        playerDetected = true;
        currentTakenDamage = damage;
    }

    public void TakeDamage() // TODO вынести в стейт EnemyTakeDamageState
    {
        objectRenderer.material.color = Color.red;

        StopAllCoroutines();
        StartCoroutine(TakeDamageRoutine());

        currentHealth -= currentTakenDamage;

        if (currentHealth <= 0)
            Die();
    }

    public void Attack()
    {
        if (attackTimer.IsRunning)
            return;
        
        attackTimer.Run();
        
        var projectile = Instantiate(projectilePrefab, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity);

        projectile.Init(playerDetector.Player);
    }

    private IEnumerator TakeDamageRoutine() 
    {
        float t = 0f;
        float dur = .5f;

        var start = Color.red;
        var end = defaultColor;

        while (t < dur)
        {
            var newColor = Color.Lerp(start, end, t / dur);

            objectRenderer.material.color = newColor;

            t += Time.deltaTime;

            yield return null;
        }

        objectRenderer.material.color = end;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }

    private bool IsTakingDamage()
    {
        return currentTakenDamage > 0;
    }
}