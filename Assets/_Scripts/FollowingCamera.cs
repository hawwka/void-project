using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform playerCoordinates;
    void Update()
    {
        transform.position = playerCoordinates.position;
    }
}
