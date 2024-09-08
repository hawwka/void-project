using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform playerCoordinates;
    void FixedUpdate()
    {
        transform.position = playerCoordinates.position;
    }
}
