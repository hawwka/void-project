using UnityEngine;

public class Constructuble : MonoBehaviour
{
    public Marker Marker;
    public GameObject Model;
    

    public void Construct()
    {
        Marker.gameObject.SetActive(false);
        Model.SetActive(true);
    }
}
