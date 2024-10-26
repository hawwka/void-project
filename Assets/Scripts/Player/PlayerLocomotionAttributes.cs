using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerLocomotionAttributes", menuName = "Player/PlayerLocomotionAttributes")]
public class PlayerLocomotionAttributes : ScriptableObject
{
    public float MovementSpeed = 8;
    public float DashForce = 50;
    public float DashDuration = 0.25f;
    public float RotationSpeed = 1600f;
}