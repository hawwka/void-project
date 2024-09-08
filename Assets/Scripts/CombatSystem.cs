using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CombatSystem : MonoBehaviour
{
    private Character character;
    
    [SerializeField]
    private List<Weapon> weapons = new();
    
    private Weapon selectedWeapon;


    private void Start()
    {
        character = GetComponent<Character>();
        selectedWeapon = weapons[0];
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            BaseAttack();
    }

    private void SelectWeapon(int weaponIndex)
    {
        
    }
    
    private void BaseAttack()
    {
        if (selectedWeapon == null) return;
        
        selectedWeapon.Attack(character.WeaponSocket.position,  character.WeaponSocket.TransformDirection(Vector3.forward));
    }
}