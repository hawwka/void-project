using UnityEngine;

public class RotatorY : MonoBehaviour
{
    public float Speed = 100;

    void Update()
    {
        transform.Rotate(0, Speed * Time.deltaTime, 0);
    }
}
