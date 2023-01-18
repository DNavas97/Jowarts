using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_WizardDB", menuName = "Jowarts/WizardDB", order = 2)]
public class WizardDB : ScriptableObject
{
    public List<WizardSO> wizards;
    
    public enum WizardName
    {
        Voldemort = 0,
        Hagrid    = 1,
        Harry     = 2,
        Ron       = 3,
        Hermione  = 4,
        Draco     = 5,
        Snape     = 6,
        Gozoso    = 7
    }

    public WizardSO GetWizardByName(WizardName wizardName) => wizards.FirstOrDefault(wizard => wizard.wizardName == wizardName);
}