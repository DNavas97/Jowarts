using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsContent : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Image wizardIcon;
    [SerializeField] private Transform healthContent, manaContent, shieldContent;
    [SerializeField] private TextMeshProUGUI wizardNameText;
    
    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void UpdateHealthBar(int health, int maxHealth)
    {
        var healthSize = (float)health / (float)maxHealth;
        healthContent.localScale = new Vector3(healthSize, 1, 1);
    }

    public void UpdateFireCooldownBar(float f) => manaContent.localScale = new Vector3(f, 1, 1);

    public void UpdateShieldCooldownBar(float f) => shieldContent.localScale = new Vector3(1, f, 1);

    public void UpdatePlayerInfo(WizardSO wizard)
    {
        wizardNameText.text = wizard.wizardName.ToString();
        wizardIcon.sprite = wizard.wizardIcon;
    }

    #endregion
}
