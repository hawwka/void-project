using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent Agent;
    public Renderer ObjectRenderer;

    [Space]
    public int MaxHealth = 100;
 
    private Player player;
    private int currentHealth;
    private Color defaultColor;

    
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        currentHealth = MaxHealth;
        ObjectRenderer = GetComponent<Renderer>();
        defaultColor = ObjectRenderer.material.color;

        // Agent.enabled = false;
        // yield return null;
        // Agent.enabled = true;
    }
    
    private void Update()
    {
        var offseet = (player.transform.position - transform.position).normalized * 2;
        Agent.SetDestination(player.transform.position - offseet);
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
