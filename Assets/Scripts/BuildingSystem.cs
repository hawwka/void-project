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
    Quaternion currentBuildingRotation;


    bool isRotatingLeft;
    bool isRotatingRight;


    public void SelectBuilding(int index = 0)
    {
        if (index >= Buildings.Count || index < 0) return;

        HideBuilding();

        currentBuilding = Instantiate(Buildings[index], Buildings[index].transform.position, Quaternion.identity);

        currentBuildingIndex = index;
    }

    public void Place(bool value)
    {
        if (!currentBuilding.Phantom.HasValidPlacement) return;

        currentBuilding.Construct();

        currentBuilding = null;

        SelectBuilding(currentBuildingIndex);
    }

    public void HideBuilding()
    {
        if (currentBuilding == null) return;

        Destroy(currentBuilding.gameObject);
    }

    void RotateLeft()
    {
        currentBuilding.transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        
        currentBuildingRotation = currentBuilding.transform.rotation;
    }

    void RotateRight()
    {
        currentBuilding.transform.Rotate(Vector3.up, -RotationSpeed * Time.deltaTime);
        
        currentBuildingRotation = currentBuilding.transform.rotation;
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
        currentBuilding.transform.rotation = currentBuildingRotation;
    }

    public void HandleRotationLeft(bool value)
    {
        isRotatingLeft = value;
    }

    public void HandleRotationRight(bool value)
    {
        isRotatingRight = value;
    }

}