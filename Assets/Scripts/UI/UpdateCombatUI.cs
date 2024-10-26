using UnityEngine;
using UnityEngine.UIElements;

public class UpdateCombatUI : MonoBehaviour
{
    [SerializeField] PlayerWeapon playerWeapon;

    UIDocument uiDocument;
    Label ammoLabel;

    public void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        
        ammoLabel = uiDocument.rootVisualElement.Q<Label>("AmmosLabel");
    }

    public void Update()
    {
        ammoLabel.text = playerWeapon.Selected.Magazine.Remaining + "/" + playerWeapon.Selected.Magazine.Capacity;
    }
}