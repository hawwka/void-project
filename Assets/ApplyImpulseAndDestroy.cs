using System.Linq;
using UnityEngine;

public class ApplyImpulseAndDestroy : MonoBehaviour
{
    Vector3 impulse;

    public float ImpulseForce = 10;
    public float DestroyTime = 5;
    
    public void Apply()
    {
        var allGameObjects = GetComponentsInChildren<Transform>();
        
        foreach (var go in allGameObjects)
        {
            go.transform.parent = null;
            
            
            impulse = -transform.forward * ImpulseForce;
            
            var rb = go.gameObject.AddComponent<Rigidbody>();
            
            rb.useGravity = true;
            
            rb.AddForce(impulse, ForceMode.Impulse);
            
            
            Destroy(go.gameObject, DestroyTime);
        }
    }
}
