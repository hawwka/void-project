using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UpdateCombatUI : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    UIDocument uiDocument;
    Label ammosLabel;

    public void Start()
    {
        TryGetComponent(out uiDocument);
        ammosLabel = uiDocument.rootVisualElement.Q<Label>("AmmosLabel");
    }

    public void Update()
    {
        ammosLabel.text = playerController.SelectedWeapon.ShotsInMagazine + "/" +
                          playerController.SelectedWeapon.MagazineCapacity;
    }
}
