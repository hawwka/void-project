using UnityEngine;
using UnityEngine.UIElements;

public class UpdateHealthUI : MonoBehaviour
{
    public Health Health;

    UIDocument uiDocument;
    Label healthPointsLabel;

    public void Start()
    {
        TryGetComponent(out uiDocument);
        healthPointsLabel = uiDocument.rootVisualElement.Q<Label>("HealthPointsLabel");
    }

    public void Update()
    {
        healthPointsLabel.text = ((int)Health.CurrentHealth).ToString();
    }
}