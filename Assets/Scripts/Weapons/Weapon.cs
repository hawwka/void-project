using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponConfigSO WeaponConfigSo;
    
    [SerializeField] 
    protected WeaponVisualEffect visualEffect;

    public abstract void Attack(Vector3 origin, Vector3 dir);
}