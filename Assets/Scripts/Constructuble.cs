using UnityEngine;

public class Constructuble : MonoBehaviour
{
    public PhantomBuilding Phantom;
    public GameObject Model;
    

    public void Construct()
    {
        Phantom.gameObject.SetActive(false);
        Model.SetActive(true);
    }
    
    public void Deconstruct()
    {
        Destroy(gameObject);
    }
}
