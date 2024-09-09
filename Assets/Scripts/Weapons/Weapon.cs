using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponConfigSO WeaponConfigSo;

    [SerializeField] 
    public Transform weaponSocket;
    
    [SerializeField] 
    protected WeaponVisualEffect visualEffect;


    public abstract void Attack();
}