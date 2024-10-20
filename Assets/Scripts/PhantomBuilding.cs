using UnityEngine;

public class PhantomBuilding : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    
    [SerializeField]
    Renderer renderer;
    
    
    int collisions = 0;

    public float Alpha = 100f;
    public bool HasValidPlacement => collisions == 0;
    
    
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
        if (!HasValidPlacement)
        {
            renderer.material.color = new Color(Color.red.r, Color.red.g, Color.red.b, .5f);
        }
        else
        {
            renderer.material.color = new Color(Color.green.r, Color.green.g, Color.green.b, .5f);
        }
    }
}