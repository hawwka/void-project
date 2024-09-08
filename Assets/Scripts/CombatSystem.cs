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
    
    private void BaseAttack()
    {
        if (selectedWeapon == null) return;
        
        selectedWeapon.Attack(character.WeaponSocket.position,  character.WeaponSocket.TransformDirection(Vector3.forward));
    }
}