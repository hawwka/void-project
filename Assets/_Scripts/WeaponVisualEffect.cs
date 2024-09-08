using System.Collections;
using UnityEngine;

public class WeaponVisualEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject TracerObject;

    public void ShowTracer(Vector3 from, Vector3 dir, float distance, float speed)
    {
        StartCoroutine(ShowTracerRoutine(from, dir, distance, speed));
    }

    private IEnumerator ShowTracerRoutine(Vector3 from, Vector3 dir, float distance, float speed)
    {
        var to = dir * distance;
        
        var tracerInstance = Instantiate(TracerObject, from, Quaternion.LookRotation(to));

        var duration = Vector3.Distance(from, to) / speed;
        
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            
            tracerInstance.transform.position += dir * (speed * Time.deltaTime);

            yield return null;
        }
        
        Destroy(tracerInstance);
    }
}