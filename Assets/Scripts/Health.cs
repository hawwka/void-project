using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    public float CurrentHealth { get; private set; }

    public UnityEvent OnHealthEnded = new();


    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            OnHealthEnded.Invoke();
        }
    }
}
