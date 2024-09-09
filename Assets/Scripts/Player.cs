using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerInputReader input;
    

    private void Start() => input.EnablePlayerActions();
}