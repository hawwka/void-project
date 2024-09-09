using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class WeaponController : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerInputReader input;
    
    [SerializeField]
    private List<Weapon> weapons = new();
    
    private Weapon selectedWeapon;


    private bool isFiring;
    private void Start()
    {
        selectedWeapon = weapons[0];
    }

    private void OnEnable()
    {
        input.Fire += HandleFire;
    }

    private void OnDisable()
    {
        input.Fire -= HandleFire;
    }

    private void HandleFire(bool isPressed)
    {
        isFiring = isPressed;
    }
    
    private void Update()
    {
        if (isFiring)
            selectedWeapon.Attack();
        
        
        if (Input.GetKey(KeyCode.Alpha1))
            SelectWeapon(0);
        
        if (Input.GetKey(KeyCode.Alpha2))
            SelectWeapon(1);
    }
    
    private void SelectWeapon(int weaponIndex)
    {
        selectedWeapon.gameObject.SetActive(false);

        selectedWeapon = weapons[weaponIndex];
        
        selectedWeapon.gameObject.SetActive(true);
    }
}