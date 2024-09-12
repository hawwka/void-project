using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] NavMeshAgent Agent;

    [SerializeField] Renderer ObjectRenderer;
    [SerializeField] EnemyProjectile Projectile;

    [Header("Settings")]
    [SerializeField] int MaxHealth = 100;

    [SerializeField] float WalkableDistance = 2;
    [SerializeField] float AttackRange = 5;
    [SerializeField] float DetectingDistance = 10;
    [SerializeField] float DetectingAngle = 30;
    [SerializeField] float AttackCooldown = 1f;
    [SerializeField] float DetectionCooldown = 1f;
    
    PlayerController playerController;
    int currentHealth;
    Color defaultColor;
    Timer attackTimer;


    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        currentHealth = MaxHealth;
        ObjectRenderer = GetComponent<Renderer>();
        defaultColor = ObjectRenderer.material.color;

        attackTimer = new Timer(AttackCooldown);
    }

    private void Update()
    {
        if (Vector3.Distance(playerController.transform.position, transform.position) < AttackRange + .1f)
        {
            transform.LookAt(new Vector3(playerController.transform.position.x, transform.position.y, playerController.transform.position.z));
            
            if (!attackTimer.IsRunning)
            {
                attackTimer.Run();
                Attack();
            }
        }

        var dir = (playerController.transform.position - transform.position);

        if (Vector3.Distance(transform.position, playerController.transform.position) > AttackRange)
            Agent.SetDestination(playerController.transform.position - dir.normalized * AttackRange);


        attackTimer.Tick(Time.deltaTime);
    }

    private void Attack()
    {
        var projectile = Instantiate(Projectile, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity);

        projectile.Init(playerController);
    }

    public void TakeDamage(int damage)
    {
        ObjectRenderer.material.color = Color.red;

        StopAllCoroutines();
        StartCoroutine(TakeDamageRoutine());

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
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

            ObjectRenderer.material.color = newColor;

            t += Time.deltaTime;

            yield return null;
        }

        ObjectRenderer.material.color = end;
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}