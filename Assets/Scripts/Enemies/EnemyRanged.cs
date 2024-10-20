using System.Collections;
using UnityEngine;
using UnityEngine.AI;



public class EnemyRanged : EnemyBase
{
    [Header("References")]
    [SerializeField] Renderer objectRenderer;
    [SerializeField] EnemyProjectile projectilePrefab;
    
    [Header("Settings")]
    [SerializeField] int maxHealth = 100;

    [SerializeField] float attackCooldown = 1f;
    
    public bool isDamageTaken;
    
    PlayerDetector playerDetector;
    NavMeshAgent agent;
    int currentHealth;
    Timer attackTimer;
    StateMachine stateMachine;
    
    Color defaultColor;
    
    IAttackStrategy attackStrategy;

    protected override void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerDetector = GetComponent<PlayerDetector>();
    }
    
    protected override void Update()
    {
        stateMachine.Update();

        attackTimer.Tick(Time.deltaTime);
    }
    
    protected override void FixedUpdate() => stateMachine.FixedUpdate();
    
    protected override void Start()
    {
        base.Start();
        currentHealth = maxHealth;
        objectRenderer = GetComponent<Renderer>();
        defaultColor = objectRenderer.material.color;

        attackTimer = new Timer(attackCooldown);
    }
    
    protected override void SetupStateMachine()
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

        stateMachine.AddAnyTransition(takeDamageState, new FuncPredicate(() => isDamageTaken));
        stateMachine.AddTransition(takeDamageState, chaseState, new FuncPredicate(() => !isDamageTaken));

        stateMachine.SetState(wanderState);
    }
    
    public override void TakeDamage(int damage)
    {
        isDamageTaken = true;

        currentHealth -= damage;
    }

    public override void Attack()
    {
        if (attackTimer.IsRunning)
            return;

        attackTimer.Run();

        var projectile = Instantiate(projectilePrefab, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity);

        projectile.Init(playerDetector.Player);
    }
    
    public override void ProcessDamageTaken()
    {
        isDamageTaken = false;

        #region TakingDamageLogic

        objectRenderer.material.color = Color.red;

        StopAllCoroutines();
        StartCoroutine(TakeDamageRoutine());

        #endregion

        if (currentHealth <= 0)
            Die();
    }
    
    IEnumerator TakeDamageRoutine()
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
    
    void Die()
    {
        Destroy(gameObject);
    }
}