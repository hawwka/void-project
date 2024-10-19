using System;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    public GameObject Building;
    public ConstructionMarker Marker;

    [SerializeField] PlayerInputReader input;
    
    
    public bool InBuildMode = false;
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            InBuildMode = !InBuildMode;
        }


        if (!InBuildMode)
        {
            Marker.gameObject.SetActive(false);
            return;   
        }
        
        var mousePos = Helpers.MouseToWorldPostion(input.MousePosition);
        
        Marker.transform.position = new Vector3(Mathf.RoundToInt(mousePos.x), mousePos.y, Mathf.RoundToInt(mousePos.z));
        Marker.gameObject.SetActive(true);

        
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Marker.transform.Rotate(Vector3.up, 90);
        }

        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
        {
            Marker.transform.Rotate(Vector3.up, -90);
        }
        
        

        
        if (Input.GetMouseButtonDown(0) && !Marker.IsColliding)
        {
            var building = Instantiate(Building, Marker.transform.position, Marker.transform.rotation);
        }

    }
    
    
    
}