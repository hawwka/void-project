using UnityEngine;
using UnityEngine.UIElements;

public class UpdateCombatUI : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Health health;

    UIDocument uiDocument;
    Label ammosLabel;
    Label healthPointsLabel;

    public void Start()
    {
        TryGetComponent(out uiDocument);
        ammosLabel = uiDocument.rootVisualElement.Q<Label>("AmmosLabel");
        healthPointsLabel = uiDocument.rootVisualElement.Q<Label>("HealthPointsLabel");
    }

    public void Update()
    {
        ammosLabel.text = playerController.SelectedWeapon.ShotsInMagazine + "/" +
                          playerController.SelectedWeapon.MagazineCapacity;

        healthPointsLabel.text = ((int)health.currentHealth).ToString();
    }
}
