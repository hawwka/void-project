using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    
    [SerializeField]
    Renderer renderer;
    
    int collisions = 0;

    public bool IsColliding => collisions > 0;
    
    
    void OnTriggerEnter(Collider other)
    {
        collisions++;
        
        SetColor();
    }

    void OnTriggerExit(Collider other)
    {
        collisions--;
        
        SetColor();
    }
    
    void SetColor()
    {
        if (IsColliding)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.green;
        }
    }
    
}