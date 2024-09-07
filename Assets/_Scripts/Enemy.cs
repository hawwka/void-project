using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int MaxHealth = 100;
    
    private int currentHealth;
    
    [SerializeField]
    private Renderer objectRenderer;

    private Color defaultColor;

    private void Start()
    {
        currentHealth = MaxHealth;
        
        objectRenderer = GetComponent<Renderer>();

        defaultColor = objectRenderer.material.color;
    }

    public void TakeDamage(int damage)
    {
        objectRenderer.material.color = Color.red;

        StopAllCoroutines();
        StartCoroutine(TakeDamageRoutine(damage));
    }

    private IEnumerator TakeDamageRoutine(int damage)
    {
        float t = 0f;
        float dur = .5f;
        
        var start = Color.red;
        var end = defaultColor;
        
        while (t < dur)
        {
            Color newColor = Color.Lerp(start, end, t / dur);

            objectRenderer.material.color = newColor;

            t += Time.deltaTime;

            yield return null;
        }

        objectRenderer.material.color = end;
        
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }


    private void Die()
    {
        print("Died beautifully!");
        
        Destroy(gameObject);
    }
}
