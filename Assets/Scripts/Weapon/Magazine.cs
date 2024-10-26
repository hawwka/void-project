using UnityEngine;

[CreateAssetMenu(fileName = "Magazine", menuName = "Weapon/Magazine")]
public class Magazine : ScriptableObject
{
    public int Remaining;
    public int Capacity;
    public float ReloadTime;
}