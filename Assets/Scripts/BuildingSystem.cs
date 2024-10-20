using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] 
    PlayerInputReader input;
    
    public List<Constructuble> Buildings;
    public float RotationSpeed = 100;

    Constructuble currentBuilding;
    int currentBuildingIndex;
    
    BuildingState buildingState;
    
    bool isRotatingLeft;
    bool isRotatingRight;

    
    public void SelectBuilding(int index = 0)
    {
        if (index >= Buildings.Count || index < 0) return;
        
        HideBuilding();
        
        currentBuilding = Instantiate(Buildings[index], Buildings[index].transform.position, Quaternion.identity);
        
        currentBuildingIndex = index;
    }
    
    public void HideBuilding()
    {
        if (currentBuilding == null) return;
        
        Destroy(currentBuilding.gameObject);
    }
    
    public void RotateLeft()
    {
        if (Input.GetKey(KeyCode.Comma))
        {
            currentBuilding.transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        }
    }
    
    public void RotateRight()
    {
        if (Input.GetKey(KeyCode.Period))
        {
            currentBuilding.transform.Rotate(Vector3.up, -RotationSpeed * Time.deltaTime);
        }
    }
    
    void Update()
    {
        if (!currentBuilding) 
            return;
        
        if (isRotatingLeft)
        {
            RotateLeft();
        }
        if (isRotatingRight)
        {
            RotateRight();
        }
        
        var mousePos = Helpers.MouseToWorldPostion(input.MousePosition);
        
        currentBuilding.transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
    }
    
    public void HandleRotationLeft(bool value)
    {
        isRotatingLeft = value;
    }
    
    public void HandleRotationRight(bool value)
    {
        isRotatingRight = value;
    }

    public void Place(bool value)
    {
        if (currentBuilding.Marker.IsColliding) return;
        
        currentBuilding.Construct();
        
        currentBuilding = null;
        
        SelectBuilding(currentBuildingIndex);
    }
}