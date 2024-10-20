using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] 
    PlayerInputReader input;
    
    public List<Constructuble> Buildings;
    public float RotationSpeed = 100;

    Constructuble selectedBuilding;
    bool isBuildMode;
    
    
    void SelectBuilding(int index)
    {
        if (index >= Buildings.Count || index < 0) return;
        
        HideBuilding();
        
        selectedBuilding = Instantiate(Buildings[index], Buildings[index].transform.position, Quaternion.identity);
    }
    
    void HideBuilding()
    {
        if (selectedBuilding == null) return;
        
        Destroy(selectedBuilding.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBuildMode = !isBuildMode;
            
            SelectBuilding(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectBuilding(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectBuilding(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectBuilding(2);
        }

        if (!isBuildMode)
        {
            HideBuilding();
            return;
        }
        
        var mousePos = Helpers.MouseToWorldPostion(input.MousePosition);
        

        selectedBuilding.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
        selectedBuilding.gameObject.SetActive(true);
        
        
        if (Input.GetKey(KeyCode.Comma))
        {
            selectedBuilding.transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.Period))
        {
            selectedBuilding.transform.Rotate(Vector3.up, -RotationSpeed * Time.deltaTime);
        }

        if (!Input.GetMouseButtonDown(0) || selectedBuilding.Marker.IsColliding) return;
        
        selectedBuilding.Construct();
        selectedBuilding = null;
        SelectBuilding(0);
    }

}