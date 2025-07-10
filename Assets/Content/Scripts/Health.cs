using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth { get; private set; }

    public UnityEvent OnHealthEnd = new();
    public UnityEvent<int> OnHealthAdded = new();
    public UnityEvent<int> OnHealthDecreased = new();


    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void AddHealth(int amount)
    {
        if (CurrentHealth <= 0)
            return;
        
        CurrentHealth += amount;

        if (CurrentHealth <= 0)
        {
            OnHealthEnd.Invoke();
        }
        
        if (amount > 0)
            OnHealthAdded.Invoke(amount);
        else
            OnHealthDecreased.Invoke(amount);
    }
}
