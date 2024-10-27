using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    public GameObject SpawnPrefab;
    public Transform Parent;

    [Header("SpawnSettings")]
    public bool SpawnAtStartup;
    public int Number = 5;
    public float SpawnRadius = 5f;
    
    
    private void Start()
    {
        if (SpawnAtStartup)
            Spawn();
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        for (int i = 0; i < Number; i++)
        {
            var randomDirection = Random.insideUnitSphere * SpawnRadius;

            NavMesh.SamplePosition(randomDirection, out var hit, SpawnRadius, 1);
            var finalPosition = hit.position;


            var parent = Parent == null ? transform : Parent;
            
            Instantiate(SpawnPrefab, finalPosition, Quaternion.Euler(0, Random.Range(0, 360), 0), parent);
        }
    }
}
