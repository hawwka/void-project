using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerInputReader input;
    public Transform WeaponSocket;

    private void Start() => input.EnablePlayerActions();
}