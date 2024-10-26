using UnityEngine;
using UnityEngine.UIElements;

public class UpdateCombatUI : MonoBehaviour
{
    public Weapon Weapon;

    UIDocument uiDocument;
    Label ammoLabel;

    public void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        
        ammoLabel = uiDocument.rootVisualElement.Q<Label>("AmmosLabel");
    }

    public void Update()
    {
        ammoLabel.text = Weapon.Magazine.Remaining + "/" +
                          0;
    }
}