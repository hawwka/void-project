using UnityEngine;

public abstract class EquipStrategy : ScriptableObject
{
    public abstract void Initialize(Weapon weapon);

    public abstract void Equip();
    
    public abstract void Unequip();
}
