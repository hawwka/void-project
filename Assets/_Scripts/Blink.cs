using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public Color Color;
    public float Duration = 0.3f;
    public Renderer[] Renderers;

    Color defaultColor;
    
    
    void Start()
    {
        defaultColor = Renderers[0].material.color;
    }
    
    public void BlinkColor()
    {
        foreach (var r in Renderers)
        {
            r.material.color = Color;
        }
        ResetColor();
    }
    
    void ResetColor()
    {
        StartCoroutine(ResetColorRoutine());
    }

    IEnumerator ResetColorRoutine()
    {
        float elapsedTime = 0;
     
        
        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;
            
            foreach (var r in Renderers)
            {
                r.material.color = Color.Lerp(Color, defaultColor, elapsedTime / Duration);
            }
            
            yield return null;
        }
    }
}
