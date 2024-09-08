using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfigSO", menuName = "Scriptable Objects/WeaponConfigSO")]
public class WeaponConfigSO : ScriptableObject
{
    public WeaponVisualEffect VisualEffect;

    public float ChargeTime;
    public float DelayAftetShot;
    public float Range;

    public int Damage;
}
