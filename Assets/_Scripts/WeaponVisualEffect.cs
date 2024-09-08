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

<<<<<<< HEAD
        var duration = distance / speed;
=======
        var duration = Vector3.Distance(from, to) / speed;
>>>>>>> c0f8f54bf8935944e254e14ac9c6ffa89a58ffc4
        
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            
            tracerInstance.transform.position += dir * (speed * Time.deltaTime);

            yield return null;
        }
        
        Destroy(tracerInstance);
    }
}