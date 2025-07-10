using UnityEngine;

[CreateAssetMenu(fileName = "Magazine", menuName = "ScriptableObjects/Weapon/Magazines")]
public class Magazine : ScriptableObject
{
    public int Remaining;
    public int Capacity;
    public float ReloadTime;
}