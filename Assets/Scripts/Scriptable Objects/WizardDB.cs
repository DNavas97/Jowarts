using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_WizardDB", menuName = "Jowarts/WizardDB", order = 2)]
public class WizardDB : ScriptableObject
{
    public List<WizardSO> wizards;
    
    public enum WizardName
    {
        [Description("Kiko Voldemoro")] Voldemort = 0,
        [Description("El Pelucas")] Hagrid    = 1,
        [Description("Parry Hotter")] Harry     = 2,
        [Description("Ron Whiskey")] Ron       = 3,
        [Description("Germayonesa")] Hermione  = 4,
        [Description("Draco Marlboro")] Draco     = 5,
        [Description("Indulgentus Snape")] Snape     = 6,
        [Description("Jovani Vazquez")] Gozoso    = 7
    }

    public WizardSO GetWizardByName(WizardName wizardName) => wizards.FirstOrDefault(wizard => wizard.wizardName == wizardName);
}