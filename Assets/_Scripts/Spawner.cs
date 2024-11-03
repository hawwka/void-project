using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject SpawnPrefab;
    public Transform Parent;
    
    public int MaxLimit = 5;
    public float SpawnInterval = 3f;
    public float InnerRadius = 10f;
    public float OuterRadius = 20f;
    
    private void Start()
    {
        InvokeRepeating(nameof(SpawnInRadius), 1,SpawnInterval);
    }
    
    void SpawnInRadius()
    {
        if (Parent != null && Parent.childCount >= MaxLimit)
        {
            return;
        }

        var randomPos = Random.insideUnitSphere.normalized * Random.Range(InnerRadius, OuterRadius) + transform.position;

        NavMesh.SamplePosition(randomPos, out var hit, 200, 1);
        
        var finalPosition = hit.position;
        var parent = Parent == null ? transform : Parent;

        Instantiate(SpawnPrefab, finalPosition, Quaternion.identity, parent);
    }
}
