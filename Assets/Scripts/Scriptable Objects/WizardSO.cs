using UnityEngine;

[CreateAssetMenu(fileName = "SO_Wizard", menuName = "Jowarts/Wizard", order = 0)]
public class WizardSO : ScriptableObject
{
    public WizardDB.WizardName wizardName;
    public Sprite wizardIcon;
    public GameObject wizardPrefab;
    public GameObject previewPrefab;
    [TextArea(5, 10)] public string description;
}