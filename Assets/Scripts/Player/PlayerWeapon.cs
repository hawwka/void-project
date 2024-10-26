using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerWeapon : MonoBehaviour
{
    public List<Weapon> Weapons = new();
    
    public float SpeedPenalty => Selected.Ergonomic;
    public Weapon Selected { get; private set; }

    
    
    void Start()
    { 
        SelectWeapon(0);
    }
    
    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex >= Weapons.Count)
            return;
        
        Selected?.Unequip();
        Selected = Weapons[weaponIndex];
        Selected.Equip();
    }

    public void Attack()
    {
        Selected.Attack();
    }
    
    public void Reload()
    {
        Selected.Reload();
    }
}