using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Image wizardIcon;
    [SerializeField] private Transform healthContent;
    [SerializeField] private TextMeshProUGUI wizardNameText;
    
    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void UpdateHealthBar(int health)
    {
        var healthSize = health / 100f;
        healthContent.localScale = new Vector3(healthSize, 1, 1);
    }

    public void UpdatePlayerInfo(WizardSO wizard)
    {
        wizardNameText.text = wizard.wizardName.ToString();
        wizardIcon.sprite = wizard.wizardIcon;
    }

    #endregion
}
