using UnityEngine;

[RequireComponent(typeof(Character))]
public class CombatSystem : MonoBehaviour
{
    private Character character;

    [SerializeField]
    private Weapon selectedWeapon;
    

    private void Start()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            BaseAttack();
    }

    private void BaseAttack()
    {
        if (selectedWeapon == null) return;
        
        selectedWeapon.Attack(character.WeaponSocket.position,  character.WeaponSocket.TransformDirection(Vector3.forward));
    }
}