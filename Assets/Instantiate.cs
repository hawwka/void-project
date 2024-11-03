using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public Transform SpawnPoint;
    public Transform Parent;
    
    public GameObject SpawnPrefab;
    
    public void Spawn()
    {
        Instantiate(SpawnPrefab, SpawnPoint.position, Quaternion.identity, Parent);
    }
}
